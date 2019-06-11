<?xml version="1.0" standalone="no"?>

<xsl:stylesheet  version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/2000/svg">
    <xsl:output
            method="xml"
            indent="yes"
            standalone="no"
            doctype-public="-//W3C//DTD SVG 1.1//EN"
            doctype-system="http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd"
            media-type="image/svg" />

    <xsl:template match="SeriesReport">
        <svg width="100%" height="9000">
            <defs xmlns="http://www.w3.org/2000/svg">
                <linearGradient id="beautyGradient">
                    <stop offset="30%" stop-color="#FFD144"/>
                    <stop offset="50%" stop-color="#D2FB1C"/>
                    <stop offset="90%" stop-color="#ECFE9C"/>
                </linearGradient>
            </defs>

            <script type="text/ecmascript">
                function showPlaylist(evt, xx) {
                    console.log(evt.target);
                    console.log(xx);
                    var elems = ["playlist1", "playlist2", "playlist0"];

                    elems.forEach(function(elem){
                        var style = document.getElementById(elem).style.display;
                        document.getElementById(elem).style.display = "none";
                    });
                    var style = document.getElementById("playlist"+xx).style.display;
                    document.getElementById("playlist"+xx).style.display = "block";
                }
            </script>

            <rect width="100%" height="100%" fill="#f1f1f1" />

            <text x="42%" y="40" writing-mode="lr-tb" font-family="sans-serif" fill="#323232" font-size="32">
                Series playlists
            </text>
            <xsl:for-each select="SeriesProviders/Playlist">
                <xsl:variable name="height" select="count(Series) * 135" />
                <xsl:variable name="precedingHeight" select="count(preceding-sibling::Playlist/Series) * 135" />
                <xsl:variable name="playlistPos" select="150" />
                <xsl:variable name="buttonXPos" select="(position() - 1) * 275 + 175" />
                <xsl:variable name="index" select="(position() - 1)" />
                <xsl:variable name="playlistOffset" select="(position() - 1) * 10" />

                <g  onclick="showPlaylist(evt, {$index})">
                    <rect x="{$buttonXPos}" y="75" width="250" height="50" fill="#904be5" stroke="#000000"/>
                    <text x="{$buttonXPos + 10}" y="105" writing-mode="lr-tb" font-family="sans-serif" fill="#121212" font-size="22">
                        <xsl:value-of select="@title"/>
                    </text>
                </g>
                <g id="playlist{$index}" display="none">
                    <rect x="50" y="{$playlistPos}" width="90%" height="{$height}" fill="#904be5" stroke="#000000" />
    <!--                <text x="70" y="{$playlistPos + 35}" writing-mode="lr-tb" font-family="sans-serif" fill="#121212" font-size="22">-->
    <!--                    <xsl:value-of select="@title"/>-->
    <!--                </text>-->
<!--                    <text x="70" y="{$playlistPos + 35}" writing-mode="lr-tb" font-family="sans-serif" fill="#121212" font-size="22">-->
<!--                        <xsl:value-of select="$precedingHeight"/>-->
<!--                    </text>-->
                    <xsl:for-each select="Series">
                        <xsl:variable name="pos" select="(position()-1) * 125"/>
                        <xsl:variable name="yOffset" select="50" />
                        <xsl:variable name="title" select="Title" />
                        <xsl:variable name="rating" select="Rating" />
                        <xsl:variable name="episodes" select="EpisodesCount" />
                        <rect x="75" y="{$playlistPos + $pos + $yOffset}" width="85%" height="100" fill="#a770ed" stroke="#323232" stroke-width="0.5" />
    <!--                    <text x="80" y="{$playlistPos + $pos + $yOffset}" fill="#121212" font-size="16">-->
    <!--                        <xsl:value-of select="$pos"/>-->
    <!--                    </text>-->
                        <text x="90" y="{$playlistPos + $pos + $yOffset + 20}" fill="#121212" font-size="17" font-family="sans-serif">
                            <xsl:value-of select="$title"/>
                        </text>

                        <text x="110" y="{$playlistPos + $pos + $yOffset + 55}" fill="#121212" font-size="17" font-family="sans-serif">
                            <xsl:value-of select="$episodes"/> Episodes
                            <animate attributeType="XML" attributeName="x" from="95" to="{110 + ($episodes*10)}"
                                     dur="1s" fill="freeze"/>
                        </text>
                        <rect x="100" y="{$playlistPos + $pos + $yOffset + 40}" height="20" width="0" fill="#ffd144"
                              stroke="#323232" stroke-width="0.5">
                            <animate attributeType="XML" attributeName="width" from="0" to="{$episodes*10}"
                                     dur="1s" fill="freeze"/>
                        </rect>

                        <text x="110" y="{$playlistPos + $pos + $yOffset + 80}" fill="#121212" font-size="17" font-family="sans-serif">
                            Rating: <xsl:value-of select="$rating"/>
                            <animate attributeType="XML" attributeName="x" from="95" to="{110 + ($rating*8)}"
                                     dur="1s" fill="freeze"/>
                        </text>
                        <rect x="100" y="{$playlistPos + $pos + $yOffset + 65}" height="20" width="0" fill="#dafc43"
                              stroke="#323232" stroke-width="0.5">
                            <animate attributeType="XML" attributeName="width" from="0" to="{$rating*8}"
                                     dur="1s" fill="freeze"/>
                        </rect>
                    </xsl:for-each>
                </g>
            </xsl:for-each>

            <text x="40%" y="1150" writing-mode="lr-tb" font-family="sans-serif" fill="#323232" font-size="32">
                Data aggregations
            </text>

            <rect x="50" y="1200" width="400" height="400" fill="#A770ED" stroke="#000000" />
            <text x="60" y="1230" writing-mode="lr-tb" font-family="sans-serif" fill="#121212" font-size="22">
                Playlist mean ratings
            </text>
            <xsl:for-each select="Aggregations/PlaylistMeanRatings/MeanRating">
                <xsl:variable name="ratingPos" select="(position() - 1) * 100 + 1300" />
                <xsl:variable name="rating" select="." />
                <rect x="70" y="{$ratingPos}" width="{$rating * 4}" height="30" fill="url(#beautyGradient)" stroke="#000000" />
                <text x="90" y="{$ratingPos - 10}" fill="#121212" font-size="17" font-family="sans-serif">
                    <xsl:value-of select="@name"/>
                </text>
            </xsl:for-each>



            <rect x="550" y="1200" width="600" height="400" fill="#A770ED" stroke="#000000" />
            <text x="560" y="1230" writing-mode="lr-tb" font-family="sans-serif" fill="#121212" font-size="22">
                Playlist monthly costs
            </text>
            <xsl:for-each select="Aggregations/PlaylistMonthlyCost/MonthlyCost">
                <xsl:variable name="ratingPos" select="(position() - 1) * 100 + 1300" />
                <xsl:variable name="price" select="cost/rawCost" />
                <xsl:variable name="tax" select="cost/taxValue" />
                <xsl:variable name="rating" select="." />
                <rect x="580" y="{$ratingPos}" width="{$price * 4}" height="30" fill="#CCF900" stroke="#000000" />
                <rect x="{580 + ($price*4)}" y="{$ratingPos}" width="{$tax * 4}" height="30" fill="#FFC000" stroke="#000000" />
                <text x="590" y="{$ratingPos - 10}" fill="#121212" font-size="17" font-family="sans-serif">
                    <xsl:value-of select="@name"/>
                </text>
            </xsl:for-each>


            <text x="42%" y="1670" writing-mode="lr-tb" font-family="sans-serif" fill="#323232" font-size="32">
                Provider series count
            </text>

            <xsl:for-each select="Aggregations/SeriesOfferedByProviders/ProviderSeries">
                <xsl:variable name="pos" select="(position() - 1) * 200 + 300" />
                <xsl:variable name="r" select="count(offeredSeries) * 10"/>

                <text x="{$pos - 40}" y="1730" writing-mode="lr-tb" font-family="sans-serif" fill="#323232" font-size="22">
                    <xsl:value-of select="@name"/>
                </text>
                <circle cx="{$pos}" cy="1850" r="{$r}" fill="url(#beautyGradient)" stroke="#323232" stroke-width="1">
                    <animate attributeType="XML" attributeName="strokeWidth"  dur="2s" repeatCount="indefinite" from="{1}" to="{10}" fill="freeze"/>
                </circle>
            </xsl:for-each>

        </svg>
    </xsl:template>

</xsl:stylesheet>
