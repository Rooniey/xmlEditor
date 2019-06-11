<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fun="http://random.org">
    <xsl:output method="text" indent="yes"/>

    <xsl:function name="fun:table-cell">
        <xsl:param name="cellValue"/>
        <xsl:param name="width"/>
        <xsl:value-of select="concat(' ', 
                                    substring(
                                        concat($cellValue, string-join(for $i in (1 to $width) return ' ', '')),
                                        1,
                                        $width),
                                    ' ')"/>
    </xsl:function>

    <xsl:template match="/">
        <xsl:text>Series playlists&#xa;</xsl:text>
        <xsl:text>&#xa;</xsl:text>
        <xsl:for-each select="SeriesReport/SeriesProviders/Playlist">
            <xsl:text>Playlist name: </xsl:text>
            <xsl:value-of select="concat('&quot;', @title, '&quot;')"/>
            <xsl:text>&#xa;</xsl:text>
            <xsl:text>|            TITLE            | RATING | EPISODES |  PREMIERE  |   PROVIDER   | PROVIDER COST |&#xa;</xsl:text>
            <xsl:text>-----------------------------------------------------------------------------------------------&#xa;</xsl:text>
            <xsl:for-each select="Series">
                <xsl:value-of select="concat('|', fun:table-cell(Title,27), '|')"/>
                <xsl:value-of select="concat(fun:table-cell(concat(Rating, Rating/@unit),6), '|')"/>
                <xsl:value-of select="concat(fun:table-cell(EpisodesCount,8), '|')"/>
                <xsl:value-of select="concat(fun:table-cell(PremiereDate,10), '|')"/>
                <xsl:value-of select="concat(fun:table-cell(Provider/Name,12), '|')"/>
                <xsl:value-of select="concat(fun:table-cell(concat(Provider/Price,' ',Provider/Price/@currency),13), '|')"/>
                <xsl:text>&#xa;</xsl:text>
            </xsl:for-each>
            <xsl:text>&#xa;</xsl:text>
        </xsl:for-each>
        <xsl:text>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$&#xa;</xsl:text>

        <xsl:text>&#xa;Playlists monthly cost&#xa;</xsl:text>
        <xsl:text>|      PLAYLIST NAME      | RAW COST | TAX VALUE |&#xa;</xsl:text>
        <xsl:text>--------------------------------------------------&#xa;</xsl:text>
        <xsl:for-each select="SeriesReport/Aggregations/PlaylistMonthlyCost/MonthlyCost">
            <xsl:value-of select="concat('|', fun:table-cell(@name,23), '|')"/>
            <xsl:value-of select="concat(fun:table-cell(concat(cost/rawCost, ' ', cost/@currency),8), '|')"/>
            <xsl:value-of select="concat(fun:table-cell(concat(cost/taxValue, ' ', cost/@currency),9), '|')"/>
            <xsl:text>&#xa;</xsl:text>
        </xsl:for-each>
        <xsl:text>&#xa;$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$&#xa;</xsl:text>
        
        <xsl:text>&#xa;Playlists episodes count&#xa;</xsl:text>
        <xsl:text>|      PLAYLIST NAME      | EPISODES COUNT |&#xa;</xsl:text>
        <xsl:text>--------------------------------------------&#xa;</xsl:text>
        <xsl:for-each select="SeriesReport/Aggregations/PlaylistEpisodeCounts/PlaylistEpisodeCount">
            <xsl:value-of select="concat('|', fun:table-cell(@name,23), '|')"/>
            <xsl:value-of select="concat(fun:table-cell(.,14), '|')"/>
            <xsl:text>&#xa;</xsl:text>
        </xsl:for-each>
        <xsl:text>&#xa;$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$&#xa;</xsl:text>

        <xsl:text>&#xa;Playlists ordered by mean ratings descending&#xa;</xsl:text>
        <xsl:text>| POSITION |      PLAYLIST NAME      | MEAN RATING |&#xa;</xsl:text>
        <xsl:text>----------------------------------------------------&#xa;</xsl:text>
        <xsl:for-each select="SeriesReport/Aggregations/PlaylistMeanRatings/MeanRating">
            <xsl:value-of select="concat('|', fun:table-cell(position(),8), '|')"/>
            <xsl:value-of select="concat(fun:table-cell(@name,23), '|')"/>
            <xsl:value-of select="concat(fun:table-cell(.,11), '|')"/>
            <xsl:text>&#xa;</xsl:text>
        </xsl:for-each>
        <xsl:text>&#xa;$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$&#xa;</xsl:text>

        <xsl:text>&#xa;Highest and Lowest rating series&#xa;</xsl:text>
        <xsl:value-of select="concat('Highest rated series: ', SeriesReport/Aggregations/HighestRatedSeries/title, ' with ', SeriesReport/Aggregations/HighestRatedSeries/rating, ' rating')"/>
        <xsl:text>&#xa;</xsl:text>
        <xsl:value-of select="concat('Lowest rated series: ', SeriesReport/Aggregations/LowestRatedSeries/title, ' with ', SeriesReport/Aggregations/LowestRatedSeries/rating, ' rating')"/>
        <xsl:text>&#xa;&#xa;$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$&#xa;&#xa;</xsl:text>


        <xsl:text>Providers&#xa;</xsl:text>
        <xsl:text>&#xa;</xsl:text>
        <xsl:for-each select="SeriesReport/Aggregations/SeriesOfferedByProviders/ProviderSeries">
            <xsl:text>Provider name: </xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>&#xa;</xsl:text>
            <xsl:for-each select="offeredSeries">
                <xsl:value-of select="concat('- ', .)"/>
                <xsl:text>&#xa;</xsl:text>
            </xsl:for-each>
            <xsl:text>&#xa;</xsl:text>
        </xsl:for-each>
        <xsl:text>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$&#xa;</xsl:text>

    </xsl:template>
</xsl:stylesheet>