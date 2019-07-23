namespace ColorantsChangeLMaget
{
    public class SqlList
    {
        private string _result = string.Empty; 
      
        /// <summary>
        /// 获取结果集
        /// </summary>
        /// <returns></returns>
        public string Get_Record(string brandName,int productId)
        {
            _result = $@"
                                BEGIN

                                SELECT  a.InnerColorId,d.ColorantId,a.ColorCode+'&'+CONVERT(VARCHAR(30),b1.FormulaVersionDate,111) '内部色号',
                                        d.ColorantCode '色母编码',d.ColorantDensity '色母密度',
		                                ISNULL(c.Weight, c.WeightPercent) '色母量(G)',CAST(0 as decimal(18, 6)) '色母量(G)之和',
		                                CAST(0 as decimal(18, 6)) '色母量(KG)',
		                                CAST(0 as decimal(18, 6)) '1KG配方各色母体积',
		                                CAST(0 as decimal(18, 6)) '1KG配方各色母体积之和',
		                                CAST(0 as decimal(18, 6)) '1KG体积比'
                                         --CAST(0 as decimal(18, 6)) '色母量(L)中间值之和',CAST(0 as decimal(18, 6)) '色母量(L)',
		                                --CAST(0 AS DECIMAL(18, 6)) '色母量(L)之和',
		                                --CAST(0 as decimal(18, 6)) '体积占比'

                                        INTO #a

                                FROM dbo.InnerColor a
                                INNER JOIN dbo.ColorFormula b ON a.InnerColorId = b.InnerColorId
                                INNER JOIN dbo.Formula b1 ON b.FormulaId = b1.FormulaId
                                INNER JOIN dbo.ColorFormulaColorants c ON b.ColorFormulaId = c.ColorFormulaId
                                INNER JOIN dbo.Colorants d ON c.ColorantsId = d.ColorantId
                                INNER JOIN dbo.BrandProduct E ON D.ProductId = E.ProductId
                                INNER JOIN dbo.Brand F ON E.BrandId = F.BrandId

                                --获取涂层信息
                                INNER JOIN dbo.ColorType G ON A.ColorTypeId = G.ColorTypeId

                                WHERE A.RelationId = 1--1为OEM配方 其它的都为车队配方
                                AND F.BrandName ='{brandName}' --'Perfecoat'--品牌参数
                                AND   E.ProductId ='{productId}' --8--产品系列参数(8:1K 7:2K) 注: 根据品牌不同,这里的产品系列ID也会不同
                                --AND a.InnerColorId = '763'--'46279'--'66810'--内部色号ID
                                --AND b.LayerNumber = 1--层
                                --AND b1.FormulaVersionDate = '2018-08-14 00:00:00.000'--版本日期
                                --AND g.ColorTypeId = '1'
                                ORDER BY d.ColorantId,b1.FormulaVersionDate,g.ColorTypeId

                                --求色母量(G)之和
                                UPDATE #a SET [色母量(G)之和]=ab.[色母量(G)之和]
                                FROM #a
                                INNER JOIN(
                                                SELECT a1.InnerColorId, ROUND(SUM(CONVERT(DECIMAL(38, 6), a1.[色母量(G)])), 3) '色母量(G)之和'

                                                FROM #a a1
				                                GROUP BY a1.InnerColorId
                                            )AS ab ON #a.InnerColorId=ab.InnerColorId

                                --计算色母量(KG)
                                UPDATE #a SET [色母量(KG)]=aa.[色母量(KG)]
                                FROM #a
                                INNER JOIN (
                                                SELECT b.InnerColorId, b.ColorantId, ROUND(b.[色母量(G)] / b.[色母量(G)之和] * 1000, 2) '色母量(KG)'

                                                FROM #a b
			                                ) AS aa ON #a.InnerColorId=aa.InnerColorId AND #a.ColorantId=aa.ColorantId

                                --计算1KG配方各色母体积
                                UPDATE #a SET [1KG配方各色母体积]=a1.[1KG配方各色母体积]
                                from #a
                                INNER JOIN (
                                                SELECT b.InnerColorId,b.ColorantId,ROUND(b.[色母量(KG)] / b.色母密度, 2) '1KG配方各色母体积'

                                                FROM #a b 
			                                )AS a1 ON #a.InnerColorId=a1.InnerColorId AND #a.ColorantId=a1.ColorantId

                                --计算1KG配方各色母体积之和
                                UPDATE #a SET [1KG配方各色母体积之和]=a2.[1KG配方各色母体积之和]
                                FROM #a
                                INNER JOIN(
                                                SELECT c.InnerColorId, SUM(c.[1KG配方各色母体积]) '1KG配方各色母体积之和'

                                                FROM #a c
				                                GROUP BY c.InnerColorId
                                            )AS a2 ON #a.InnerColorId=a2.InnerColorId

                                --计算1KG体积比
                                UPDATE #a SET [1KG体积比]=a2.[1KG体积比]
                                FROM #a
                                INNER JOIN(
                                                SELECT d.InnerColorId, d.ColorantId, ROUND(d.[1KG配方各色母体积] / d.[1KG配方各色母体积之和] * 100, 2) '1KG体积比'

                                                FROM #a d
			                                ) AS a2 ON #a.InnerColorId=a2.InnerColorId AND #a.ColorantId=a2.ColorantId

                                --更新色母量(L)中间值之和
                                --UPDATE #a SET [色母量(L)中间值之和]=a1.[色母量(L)中间值之和]
                                --FROM #a
                                --INNER JOIN (
                                --SELECT b.InnerColorId, SUM(b.[色母量(KG)] / b.色母密度) '色母量(L)中间值之和'
                                --              FROM #a b
                                --              GROUP BY b.InnerColorId
                                --          ) AS a1 ON #a.InnerColorId=a1.InnerColorId

                                --更新色母量(L)
                                --UPDATE #a SET [色母量(L)]=a2.[色母量(L)]
                                --FROM #a
                                --INNER JOIN (
                                --SELECT c.InnerColorId, c.ColorantId, ROUND(c.[色母量(KG)] * 1000 / c.[色母量(L)中间值之和], 2) '色母量(L)'
                                --              FROM #a c
                                --          ) AS a2 ON #a.InnerColorId=a2.InnerColorId AND #a.ColorantId=a2.ColorantId

                                --更新色母量(L)之和
                                --UPDATE #a SET [色母量(L)之和]=a3.[色母量(L)之和]
                                --FROM #a
                                --INNER JOIN (
                                --SELECT b1.InnerColorId, SUM(b1.[色母量(L)]) '色母量(L)之和'
                                --              FROM #a b1
                                --              GROUP BY b1.InnerColorId
                                --          ) AS a3 ON #a.InnerColorId=a3.InnerColorId

                                --更新体积占比
                                --UPDATE #a SET 体积占比=a4.体积占比
                                --FROM #a
                                --INNER JOIN (
                                --SELECT b2.InnerColorId, b2.ColorantId, ROUND(b2.[色母量(L)] / b2.[色母量(L)之和] * 100, 2) '体积占比'
                                --              FROM #a b2
                                --          ) AS a4 ON #a.InnerColorId=a4.InnerColorId AND #a.ColorantId=a4.ColorantId


                                SELECT a.内部色号,a.色母编码,a.色母密度,a.[色母量(G)],a.[色母量(KG)],a.[1KG配方各色母体积],a.[1KG体积比]--,a.[色母量(L)],a.体积占比
                                FROM #a a
                                order by a.InnerColorId,a.ColorantId


                                --DROP TABLE #a

                                END
                        ";
            return _result;
        }
    }
}
