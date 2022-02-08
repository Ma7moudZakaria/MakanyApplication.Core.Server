using MakanyApplication.Shared.Models.DataTransferObjects.SubCategory;
using MakanyApplication.Shared.Models.ErrorHandler;
using MakanyApplication.Shared.Models.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakanyApplication.Shared.Models.Repository
{
    public class SubCategoryRepo
    {
        private readonly MakanyApplicationDbContext _dbContext;

        public SubCategoryRepo(MakanyApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IndexSubCategory>> GetDetailsAsync(int Id)
        {
            try
            {
                return new CommitResult<IndexSubCategory>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<SubCategory>().ProjectToType<IndexSubCategory>().SingleOrDefaultAsync(subCategory => subCategory.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<IndexSubCategory>
                {
                    IsSuccess = false,
                    ErrorCode = "SCAT-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexSubCategory>>> GetAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<IndexSubCategory>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<SubCategory>().Where(subCategory => !subCategory.IsDeleted).ProjectToType<IndexSubCategory>().ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexSubCategory>>
                {
                    IsSuccess = false,
                    ErrorCode = "SCAT-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<UpdateSubCategory>> GetSubCategoryForUpdateAsync(int Id)
        {
            try
            {
                return new CommitResult<UpdateSubCategory>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<SubCategory>().ProjectToType<UpdateSubCategory>().SingleOrDefaultAsync(subCategory => subCategory.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<UpdateSubCategory>
                {
                    IsSuccess = false,
                    ErrorCode = "SCAT-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<string>> CreateAsync(CreateSubCategory model)
        {
            using (IDbContextTransaction tran = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (model is null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "SCAT-X0002",
                            ErrorType = ErrorType.Error
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Name))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "SCAT-X0003",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.CategoryId <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "SCAT-X0004",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    SubCategory tempSubCategory = await _dbContext.Set<SubCategory>().Where(subCategory => subCategory.Name.Equals(model.Name) && !subCategory.IsDeleted).SingleOrDefaultAsync();

                    if (tempSubCategory != null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "SCAT-X0005",
                            ErrorType = ErrorType.Error
                        };
                    }

                    tempSubCategory = new SubCategory
                    {
                        Name = model.Name,
                        CategoryId = model.CategoryId,
                        //Picture = model.Picture,
                        IsDeleted = false
                    };

                    _dbContext.Set<SubCategory>().Add(tempSubCategory);

                    await _dbContext.SaveChangesAsync();
                    await tran.CommitAsync();

                    return new CommitResult<string>
                    {
                        IsSuccess = true,
                        ErrorCode = string.Empty,
                        ErrorType = ErrorType.None,
                        Value = "Created Successfully"
                    };
                }
                catch
                {
                    await tran.RollbackAsync();
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "SCAT-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
            }
        }

        public async Task<CommitResult<string>> UpdateAsync(UpdateSubCategory model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "SCAT-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "SCAT-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.CategoryId <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "SCAT-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                SubCategory tempSubCategory = await _dbContext.Set<SubCategory>().Where(subCategory => subCategory.Name.Equals(model.Name) && !subCategory.IsDeleted).SingleOrDefaultAsync();

                if (tempSubCategory != null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "SCAT-X0005",
                        ErrorType = ErrorType.Error
                    };
                }

                return new CommitResult<string>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = "Updated Successfully"
                };
            }
            catch
            {
                return new CommitResult<string>
                {
                    IsSuccess = false,
                    ErrorCode = "SCAT-X0001",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
