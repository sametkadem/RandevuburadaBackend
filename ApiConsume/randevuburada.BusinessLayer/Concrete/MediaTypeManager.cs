using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class MediaTypeManager : IMediaTypeService
    {
        private readonly IMediaTypeDal _mediaTypeDal;

        public MediaTypeManager(IMediaTypeDal mediaTypeDal)
        {
            _mediaTypeDal = mediaTypeDal;
        }

        public void TDelete(MediaType t)
        {
            _mediaTypeDal.Delete(t);
        }

        public MediaType TGetByID(int id)
        {
            return _mediaTypeDal.GetByID(id);
        }

        public List<MediaType> TGetList()
        {
            return _mediaTypeDal.GetList();
        }

        public void TInsert(MediaType t)
        {
            _mediaTypeDal.Insert(t);
        }

        public void TUpdate(MediaType t)
        {
            _mediaTypeDal.Update(t);
        }
    }
}
