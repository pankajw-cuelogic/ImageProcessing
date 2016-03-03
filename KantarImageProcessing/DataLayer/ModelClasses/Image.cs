using DataLayer.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.ModelClasses
{
    public class Image
    {
        ImageDBEntities ent = new ImageDBEntities();

        #region CUID Operations
        /// <summary>
        /// To get all file name list
        /// </summary>
        /// <returns>List<string></returns>
        public List<string> GetAllImagePath()
        {
            try
            {
                var v = (from t in ent.Images
                         where t.IsDeleted == false
                         select t.ImagePath);
                return v != null ? v.ToList() : null;
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        /// <summary>
        /// To save/update metadata of image files
        /// </summary>
        /// <param name="imageList"></param>
        public void SaveUpdateMetadata(List< EntityModel.Image> imageList)
        {
            try
            {
                foreach (var imageObj in imageList)
                {
                    saveMetadata(imageObj);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
     
        /// <summary>
        /// To save metadata of file
        /// </summary>
        /// <param name="obj"></param>
        private int saveMetadata(EntityModel.Image obj)
        {
            try
            {
                ent.Images.Add(obj);
                ent.SaveChanges();
                return obj.ImageId;
            }
            catch (Exception)
            {
                return 0;
            }
        }
     
        /// <summary>
        /// Get best match images from database
        /// </summary>
        /// <param name="imgObj"></param>
        /// <returns>returns list of best match images List<EntityModel.Image></returns>
        public List<EntityModel.Image> GetImagesByBestMatch(EntityModel.Image imgObj)
        {
            try
            {
                var imgList = ent.Images.Where(p => p.Checksum == imgObj.Checksum && p.ImagePath != imgObj.ImagePath).ToList();
                if (imgList.Count() != 0)
                    return imgList;

                var resultedImgList = ent.Images.Where(p => p.Length >= (imgObj.Length - 5000) && p.Length <= (imgObj.Length + 5000)
                                  && p.RedPercentage >= (imgObj.RedPercentage - 2) && p.RedPercentage <= (imgObj.RedPercentage + 2)
                                  && p.GreenPercentage >= (imgObj.GreenPercentage - 2) && p.GreenPercentage <= (imgObj.GreenPercentage + 2)
                                  && p.BluePercentage >= (imgObj.BluePercentage - 2) && p.BluePercentage <= (imgObj.BluePercentage + 2)
                                  && p.Width >= (imgObj.Width - 50) && p.Width <= (imgObj.Width + 50)
                                  && p.Height >= (imgObj.Height - 50) && p.Height <= (imgObj.Height + 50)
                                  && p.IsImageContainsFace == imgObj.IsImageContainsFace
                                  && p.IsImageContainsText == imgObj.IsImageContainsText
                                 ).ToList();

                return resultedImgList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
