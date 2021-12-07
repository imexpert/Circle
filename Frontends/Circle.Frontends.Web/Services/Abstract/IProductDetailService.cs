﻿using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Abstract
{
    public interface IProductDetailService
    {
        Task<ResponseMessage<ProductDetail>> AddAsync(AddProuctDetailModel productDetail);
        Task<ResponseMessage<List<ProductDetailModel>>> GetListAsync(Guid productId);
    }
}
