using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using SMS.Model.Products;

namespace SMS.BLL.Products
{
    public class Products
    {
        private SQLServerDAL.Products.Products dal;
        public Products()
        {
            dal = new SMS.SQLServerDAL.Products.Products();
        }

        public int Add(ProductsInfo PInfo)
        {
            return dal.Add(PInfo);
        }

        public int Del(string ID)
        {
            return dal.Del(ID);
        }

        public int DelByCategory(string CategoryID)
        {
            return dal.DelByCategory(CategoryID);
        }

        public int Save(ProductsInfo PInfo)
        {
            return dal.Save(PInfo);
        }

        public int UpdateHits(int ID)
        {
            return dal.UpdateHits(ID);
        }

        public ProductsInfo GetByID(int ID)
        {
            return dal.GetByID(ID);
        }

        public IList<ProductsInfo> GetListByCategory(int PageIndex, int PageSize, out int RecordTotal, string CategoryIDList)
        {
            return dal.GetListByCategory(PageIndex, PageSize, out RecordTotal, CategoryIDList);
        }

        public IList<ProductsInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            return dal.GetList(PageIndex, PageSize, out RecordTotal);
        }

        public ArrayList GetPictureByID(string ID)
        {
            return dal.GetPictureByID(ID);
        }

        public ArrayList GetPictureByCategory(string CategoryID)
        {
            return dal.GetPictureByCategory(CategoryID);
        }

        public void MovePicture(string UploadDir, string TempUploadFolder, string ProductsPictureFolder, string PictureFoolder)
        {
            string FullDir = System.Web.HttpContext.Current.Server.MapPath(UploadDir + ProductsPictureFolder + "\\");
            if (!System.IO.Directory.Exists(FullDir))
                System.IO.Directory.CreateDirectory(FullDir);
            System.IO.Directory.Move(System.Web.HttpContext.Current.Server.MapPath(UploadDir + TempUploadFolder + "\\" + PictureFoolder + "\\"), FullDir + PictureFoolder + "\\");
        }

        public void DelPicture(string UploadDir, string ProductsPictureFolder, string PictureFoolder)
        {
            string FullDir = System.Web.HttpContext.Current.Server.MapPath(UploadDir + ProductsPictureFolder + "\\" + PictureFoolder + "\\");
            if (System.IO.Directory.Exists(FullDir))
                System.IO.Directory.Delete(FullDir, true);
        }
    }
}
