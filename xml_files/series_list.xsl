<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0" xmlns:sp="http://tempuri.org/SeriesPlaylist">
    <xsl:key name="provider__self" match="sp:provider" use="@provider_id"/>
    <xsl:key name="series__provider" match="sp:series" use="@provider"/>
    <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
    <xsl:template match="/">
        <xsl:element name="SeriesReport">
            <xsl:element name="SeriesProviders">
                <xsl:apply-templates select="sp:seriesPlaylists/sp:playlists"/>
            </xsl:element>
            <xsl:element name="Aggregations">
                <!-- srednia ocena seriali w playliscie (posortowane)-->
                <xsl:element name="PlaylistMeanRatings">
                    <xsl:for-each select="sp:seriesPlaylists/sp:playlists/sp:playlist">
                        <xsl:sort data-type="number" select="avg(sp:positions/sp:series/sp:rottenTomatoes)" order="descending"/>
                        <xsl:element name="MeanRating">
                            <xsl:attribute name="name">
                                <xsl:value-of select="sp:name"/>
                            </xsl:attribute>
                            <xsl:value-of select="round(avg(sp:positions/sp:series/sp:rottenTomatoes) * 100) div 100"/>
                        </xsl:element>
                    </xsl:for-each>
                </xsl:element>
                <!-- najlepszy i najslabszy serial -->
                <xsl:element name="HighestRatedSeries">
                    <xsl:for-each select="//sp:series[sp:rottenTomatoes = max(//sp:rottenTomatoes)]">
                        <xsl:element name="title">
                            <xsl:value-of select="sp:title"/>
                        </xsl:element>
                        <xsl:element name="rating">
                            <xsl:value-of select="sp:rottenTomatoes"/>
                        </xsl:element>
                    </xsl:for-each>
                </xsl:element>
                <xsl:element name="LowestRatedSeries">
                    <xsl:for-each select="//sp:series[sp:rottenTomatoes = min(//sp:rottenTomatoes)]">
                        <xsl:element name="title">
                            <xsl:value-of select="sp:title"/>
                        </xsl:element>
                        <xsl:element name="rating">
                            <xsl:value-of select="sp:rottenTomatoes"/>
                        </xsl:element>
                    </xsl:for-each>
                </xsl:element>
                <!-- seriale dostępne dla providera -->
                <xsl:element name="SeriesOfferedByProviders">
                    <xsl:for-each select="//sp:provider">
                        <xsl:element name="ProviderSeries">
                            <xsl:attribute name="name">
                                <xsl:value-of select="sp:name"/>
                            </xsl:attribute>
                            <xsl:for-each select="key('series__provider', @provider_id)">
                                <xsl:element name="offeredSeries">
                                    <xsl:value-of select="sp:title"/>
                                </xsl:element>
                            </xsl:for-each>
                        </xsl:element>
                    </xsl:for-each>
                </xsl:element>
                <!-- ile jest odcinkow w playliscie -->
                <xsl:element name="PlaylistEpisodeCounts">
                    <xsl:for-each select="sp:seriesPlaylists/sp:playlists/sp:playlist">
                        <xsl:element name="PlaylistEpisodeCount">
                            <xsl:attribute name="name">
                                <xsl:value-of select="sp:name"/>
                            </xsl:attribute>
                            <xsl:value-of select="sum(sp:positions/sp:series/sp:totalNumberOfEpisodes)"/>
                        </xsl:element>
                    </xsl:for-each>
                </xsl:element>
                <!-- Cena subskrypcji potrzebna do obejrzenia playlisty -->
                <!-- ^z podatkiem(+VAT(taksacja(kradziez(wyzysk))) -->
                <xsl:element name="PlaylistMonthlyCost">
                    <xsl:for-each select="sp:seriesPlaylists/sp:playlists/sp:playlist">
                        <xsl:element name="MonthlyCost">
                            <xsl:attribute name="name">
                                <xsl:value-of select="sp:name"/>
                            </xsl:attribute>
                            <xsl:element name="cost">
                                <xsl:variable name="plnPrice" select="sum(key('provider__self', sp:positions/sp:series/@provider[not(.=preceding::sp:positions[sp:name=sp:name]/sp:series/@provider)])/sp:monthlyPayment/sp:payment[@currency='PLN'])"/>
                                <xsl:attribute name="currency">PLN</xsl:attribute>
                                <xsl:element name="rawCost">
                                    <xsl:value-of select="substring(string($plnPrice), 1, 4)"/>
                                </xsl:element>
                                <xsl:element name="taxValue">
                                    <xsl:value-of select="substring(string($plnPrice * 0.23), 1, 4)"/>
                                </xsl:element>
                            </xsl:element>
                        </xsl:element>
                    </xsl:for-each>
                </xsl:element>

                <xsl:element name="ReportDate">
                    <xsl:value-of select="format-dateTime(current-dateTime(),'[D01]-[M01]-[Y0001]')" />
                </xsl:element>
            </xsl:element>

        </xsl:element>
    </xsl:template>
    <xsl:template match="sp:seriesPlaylists/sp:playlists">
        <xsl:for-each select="sp:playlist">
            <xsl:element name="Playlist">
                <xsl:attribute name="title">
                    <xsl:value-of select="sp:name"/>
                </xsl:attribute>

                    <xsl:for-each select="sp:positions/sp:series">
                        <xsl:element name="Series">

                            <xsl:element name="Provider">

                                <xsl:element name="Name">
                                    <xsl:value-of select="key('provider__self', @provider)/sp:name"/>
                                </xsl:element>
                                <xsl:element name="Price">
                                    <xsl:attribute name="currency">PLN</xsl:attribute>
                                    <xsl:value-of select="key('provider__self', @provider)/sp:monthlyPayment/sp:payment[@currency='PLN']"/>
                                </xsl:element>
                            </xsl:element>

                            <xsl:choose>
                                <xsl:when test="../../sp:name='Trending'">
                                    <xsl:element name="Title">
                                        <xsl:value-of select="concat(sp:title, '!')"/>
                                    </xsl:element>
                                </xsl:when>
                                <xsl:otherwise>
                                    <xsl:element name="Title"><xsl:value-of select="sp:title"/></xsl:element>
                                </xsl:otherwise>
                            </xsl:choose>

                            <xsl:element name="Rating">
                                <xsl:attribute name="unit">
                                    <xsl:value-of select="sp:rottenTomatoes/@unit"/>
                                </xsl:attribute>
                                <xsl:value-of select="sp:rottenTomatoes"/>
                            </xsl:element>

                            <xsl:element name="EpisodesCount">
                                <xsl:value-of select="sp:totalNumberOfEpisodes"/>
                            </xsl:element>

                            <xsl:element name="PremiereDate">
                                <xsl:value-of select="sp:firstEpisodePremiere"></xsl:value-of>
                            </xsl:element>

                        </xsl:element>
                    </xsl:for-each>
            </xsl:element>
        </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>
