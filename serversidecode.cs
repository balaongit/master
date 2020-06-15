public string GetCategoryInfo(int categoryId)

	        {

	            string parentCategoryId = "";

	            string catName = "";

	            string catKeyword = "";

	            try

	            {

	                string statement = @"

DECLARE @param INT = categoryId

;WITH CTE AS (

   SELECT CategoryId, ParentCategoryId, Name, Keywords

   FROM Cat

   WHERE CategoryId = @param



   UNION ALL



   SELECT c1.CategoryId, c1.ParentCategoryId, c1.Name, c1.Keywords

   FROM Cat AS c1

   INNER JOIN CTE AS c2 ON c1.CategoryId = c2.ParentCategoryId  

   WHERE c1.Keywords IS NOT NULL 

)

SELECT *

FROM CTE

" ;

	                using (var conn = DBConnection.GetConnection())

	                {

	                    using (SqlCommand comm = new SqlCommand(statement, conn))

	                    {

	                        try

	                        {

	                            using (SqlDataReader dr = comm.ExecuteReader())

	                            {

	                                if (dr.HasRows)

	                                {

	                                    while (dr.Read())

	                                    {

	                                        parentCategoryId = dr["ParentCategoryId"].ToString();

	                                        catName = dr["Name"].ToString();

                                        catKeyword = dr["Keyword"].ToString();

	

	 }

	                                }

	                            }

	                        }

	                        catch (Exception ex)

	                        {





	                        }

	                        conn.Close();

	                    }

	                }

	            }

	            catch (Exception ex)

	            {





	            }

	            return !string.IsNullOrEmpty(catName) ? string.Format(@"ParentCategoryID={0}, Name={1}, Keyword={2}", parentCategoryId, catName, catKeyword) : "No Information found for this category Id.";

	        }

       public string GetCategoryLevel(int categoryLevel, int currentLevel, string parentCatId)

	        {

	            string catName = "";

	            

	            try

	            {

	                 string statement = @"

DECLARE @param INT = parentCatId

;WITH CTE AS (

   SELECT CategoryId, ParentCategoryId, Name, Keywords

   FROM Cat

   WHERE ParentCategoryId = @param



   UNION ALL



   SELECT c1.CategoryId, c1.ParentCategoryId, c1.Name, c1.Keywords

   FROM Cat AS c1

   INNER JOIN CTE AS c2 ON c1.CategoryId = c2.ParentCategoryId  

   WHERE c1.Keywords IS NOT NULL 

)

SELECT *

FROM CTE

" ;

	                using (var conn = DBConnection.GetConnection())

	                {

	                    using (SqlCommand comm = new SqlCommand(statement, conn))

	                        try

	                        {

	                            using (SqlDataReader dr = comm.ExecuteReader())

	                            {

	                                if (dr.HasRows)

	                                {

	                                    while (dr.Read())

	                                    {

	                                        if(categoryLevel == currentLevel)

	                                        {

	                                            catName = catName+ dr["Name"].ToString() + "("+ dr["CategoryId"].ToString()+"),";

	                                        }

	                                        else

	                                        {

	                                            int nextLevel = currentLevel + 1;

	                                            catName = catName+ GetCategoryLevel(categoryLevel, nextLevel, dr["CategoryId"].ToString());

	                                        }

	                                        

	                                       

	                                    }

	                                }

	                            }

	                        }

	                        catch (Exception ex)

	                        {





	                        }

	                    conn.Close();

	                }

	            }

	            catch (Exception ex)

	            {





	            }

	            return catName;

	        }

