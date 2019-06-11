<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:fo="http://www.w3.org/1999/XSL/Format">
    <xsl:output method="xml" encoding="UTF-8" indent="yes" />
    <xsl:template match="/">
        <fo:root>

            <fo:layout-master-set>
                <fo:simple-page-master master-name="series-playlists" page-height="11in" page-width="8.5in" font-family="Arial">
                    <fo:region-body region-name="body" margin="0.7in" />
                </fo:simple-page-master>
                <fo:simple-page-master master-name="providers-series" page-height="11in" page-width="8.5in" font-family="Arial">
                    <fo:region-body region-name="body" margin="0.7in" />
                </fo:simple-page-master>
                <fo:simple-page-master master-name="playlist-cost" page-height="11in" page-width="8.5in" font-family="Arial">
                    <fo:region-body region-name="body" margin="0.7in" />
                </fo:simple-page-master>
            </fo:layout-master-set>

            <fo:page-sequence master-reference="series-playlists">
                <fo:flow flow-name="body">
                    <fo:block text-align="left" font-weight="bold" font-size="15" background-color="#000000" color="#FFFFFF" padding-left="5" margin-bottom="10">SECTION 1 - Series playlist</fo:block>
                    <xsl:for-each select="SeriesReport/SeriesProviders/Playlist">
                        <fo:block text-align="center" font-weight="bold" margin-bottom="5">
                            <xsl:value-of select="concat('&quot;', @title, '&quot;')" />
                        </fo:block>
                        <fo:table width="100%" border="solid 1pt #000000" font-size="9" margin-bottom="20">
                            <fo:table-header text-align="center" font-weight="bold" color="#000000">
                                <fo:table-row border="solid 1pt #000000" background-color="#B8B8B8">
                                    <fo:table-cell width="125">
                                        <fo:block>Title</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                        <fo:block>Rating</fo:block>
                                    </fo:table-cell >
                                    <fo:table-cell >
                                        <fo:block>Premiere data</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                        <fo:block>Episodes count</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                        <fo:block width="100">Provider name</fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                        <fo:block>Provider price</fo:block>
                                    </fo:table-cell>
                                </fo:table-row>
                            </fo:table-header>
                            <xsl:for-each select="Series">
                                <fo:table-body>
                                    <fo:table-row border="solid 1pt #000000" color="#000000" text-align="center" height="25">
                                        <fo:table-cell display-align="center">
                                            <fo:block margin-left="5" text-align="left">
                                                <xsl:value-of select="Title"/>
                                            </fo:block>
                                        </fo:table-cell>
                                        <fo:table-cell display-align="center">
                                            <fo:block>
                                                <xsl:value-of select="Rating"/>
                                            </fo:block>
                                        </fo:table-cell>
                                        <fo:table-cell display-align="center">
                                            <fo:block>
                                                <xsl:value-of select="PremiereDate"/>
                                            </fo:block>
                                        </fo:table-cell>
                                        <fo:table-cell display-align="center">
                                            <fo:block>
                                                <xsl:value-of select="EpisodesCount"/>
                                            </fo:block>
                                        </fo:table-cell>
                                        <fo:table-cell display-align="center">
                                            <fo:block>
                                                <xsl:value-of select="Provider/Name"/>
                                            </fo:block>
                                        </fo:table-cell>
                                        <fo:table-cell display-align="center">
                                            <fo:block>
                                                <xsl:value-of select="concat(Provider/Price, ' ', Provider/Price/@currency)"/>
                                            </fo:block>
                                        </fo:table-cell>
                                    </fo:table-row>
                                </fo:table-body>
                            </xsl:for-each>
                        </fo:table>
                    </xsl:for-each>
                </fo:flow>
            </fo:page-sequence>

            <fo:page-sequence master-reference="providers-series">
                <fo:flow flow-name="body">
                    <fo:block text-align="left" font-weight="bold" font-size="15" background-color="#000000" color="#FFFFFF" padding-left="5" margin-bottom="10">SECTION 2 - AGGREGATIONS</fo:block>
                    <fo:block text-align="left" font-weight="bold" font-size="15" background-color="#000000" color="#FFFFFF" padding-left="5" margin-bottom="10">2.1 - Playlist monthly cost</fo:block>
                    <fo:table width="90%" margin-left="5%" border="solid 1pt #000000" font-size="9" margin-bottom="20">
                        <fo:table-header text-align="center" font-weight="bold" color="#000000" background-color="#B8B8B8">
                            <fo:table-row border="solid 1pt #000000">
                                <fo:table-cell min-width="200">
                                    <fo:block>Playlist name</fo:block>
                                </fo:table-cell>
                                <fo:table-cell>
                                    <fo:block>Raw cost</fo:block>
                                </fo:table-cell>
                                <fo:table-cell>
                                    <fo:block>Tax value</fo:block>
                                </fo:table-cell>
                            </fo:table-row>
                        </fo:table-header>
                        <fo:table-body>
                            <xsl:for-each select="SeriesReport/Aggregations/PlaylistMonthlyCost/MonthlyCost">
                                <fo:table-row border="solid 1pt #000000" color="#000000" text-align="center">
                                    <fo:table-cell>
                                        <fo:block>
                                            <xsl:value-of select="@name"/>
                                        </fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                        <fo:block>
                                            <xsl:value-of select="concat(cost/rawCost, ' ', cost/@currency)"/>
                                        </fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                        <fo:block>
                                            <xsl:value-of select="concat(cost/taxValue, ' ', cost/@currency)"/>
                                        </fo:block>
                                    </fo:table-cell>
                                </fo:table-row>
                            </xsl:for-each>
                        </fo:table-body>
                    </fo:table>

                    <fo:block text-align="left" font-weight="bold" font-size="15" background-color="#000000" color="#FFFFFF" padding-left="5" margin-bottom="10">2.2 - Playlists episodes count</fo:block>
                    <fo:table width="90%" margin-left="5%" border="solid 1pt #000000" font-size="9" margin-bottom="20">
                        <fo:table-header text-align="center" font-weight="bold" color="#000000" background-color="#B8B8B8">
                            <fo:table-row border="solid 1pt #000000">
                                <fo:table-cell min-width="250">
                                    <fo:block>Playlist name</fo:block>
                                </fo:table-cell>
                                <fo:table-cell>
                                    <fo:block width="50">Episodes count</fo:block>
                                </fo:table-cell>
                            </fo:table-row>
                        </fo:table-header>
                        <fo:table-body>
                            <xsl:for-each select="SeriesReport/Aggregations/PlaylistEpisodeCounts/PlaylistEpisodeCount">
                                <fo:table-row border="solid 1pt #000000" color="#000000" text-align="center">
                                    <fo:table-cell>
                                        <fo:block>
                                            <xsl:value-of select="@name"/>
                                        </fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                        <fo:block>
                                            <xsl:value-of select="."/>
                                        </fo:block>
                                    </fo:table-cell>
                                </fo:table-row>
                            </xsl:for-each>
                        </fo:table-body>
                    </fo:table>

                    <fo:block text-align="left" font-weight="bold" font-size="15" background-color="#000000" color="#FFFFFF" padding-left="5" margin-bottom="10">2.2 - Playlists mean rating</fo:block>
                    <fo:table width="90%" margin-left="5%" border="solid 1pt #000000" font-size="9" margin-bottom="20">
                        <fo:table-header text-align="center" font-weight="bold" color="#000000" background-color="#B8B8B8">
                            <fo:table-row border="solid 1pt #000000">
                                <fo:table-cell max-width="30">
                                    <fo:block>Position</fo:block>
                                </fo:table-cell>
                                <fo:table-cell min-width="250">
                                    <fo:block>Playlist name</fo:block>
                                </fo:table-cell>
                                <fo:table-cell>
                                    <fo:block width="50">Average rating</fo:block>
                                </fo:table-cell>
                            </fo:table-row>
                        </fo:table-header>
                        <fo:table-body>
                            <xsl:for-each select="SeriesReport/Aggregations/PlaylistMeanRatings/MeanRating">
                                <fo:table-row border="solid 1pt #000000" color="#000000" text-align="center">
                                    <fo:table-cell>
                                        <fo:block>
                                            <xsl:value-of select="position()"/>
                                        </fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                        <fo:block>
                                            <xsl:value-of select="@name"/>
                                        </fo:block>
                                    </fo:table-cell>
                                    <fo:table-cell>
                                        <fo:block>
                                            <xsl:value-of select="."/>
                                        </fo:block>
                                    </fo:table-cell>
                                </fo:table-row>
                            </xsl:for-each>
                        </fo:table-body>
                    </fo:table>

                    <fo:block text-align="left" font-weight="bold" font-size="15" background-color="#000000" color="#FFFFFF" padding-left="5" margin-bottom="10">2.2 - Highest and lowest rated series</fo:block>
                    <fo:block>
                        <xsl:value-of select="concat('Highest rated: &quot;', SeriesReport/Aggregations/HighestRatedSeries/title, '&quot; with ', SeriesReport/Aggregations/HighestRatedSeries/rating, ' rating')" />
                    </fo:block>
                    <fo:block>
                        <xsl:value-of select="concat('Lowest rated: &quot;', SeriesReport/Aggregations/LowestRatedSeries/title, '&quot; with ', SeriesReport/Aggregations/LowestRatedSeries/rating, ' rating')" />
                    </fo:block>

                </fo:flow>
            </fo:page-sequence>    

            <fo:page-sequence master-reference="playlist-cost">
                <fo:flow flow-name="body">
                    <fo:block text-align="left" font-weight="bold" font-size="15" background-color="#000000" color="#FFFFFF" padding-left="5" margin-bottom="10">SECTION 3 - Series divided by providers</fo:block>
                    <xsl:for-each select="SeriesReport/Aggregations/SeriesOfferedByProviders/ProviderSeries" >
                        <fo:block text-align="center" font-weight="bold" font-size="15" color="#000000" margin-top="15">
                            <xsl:value-of select="@name"/>
                        </fo:block>
                        <fo:list-block margin-left="50">
                            <xsl:for-each select="offeredSeries">
                                <fo:list-item>
                                    <fo:list-item-label>
                                        <fo:block>
                                            <xsl:value-of select="concat(position(),'.')"/>
                                        </fo:block>
                                    </fo:list-item-label>
                                    <fo:list-item-body>
                                        <fo:block margin-left="15">
                                            <xsl:value-of select="."/>
                                        </fo:block>
                                    </fo:list-item-body>
                                </fo:list-item>
                            </xsl:for-each>
                        </fo:list-block>
                    </xsl:for-each>
                </fo:flow>
            </fo:page-sequence>            
        </fo:root>
    </xsl:template>
</xsl:stylesheet>