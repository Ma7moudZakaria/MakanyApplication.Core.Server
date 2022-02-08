using MakanyApplication.Shared.Models.DataTransferObjects.Category;
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
    public class CategoryRepo
    {
        private readonly MakanyApplicationDbContext _dbContext;

        public CategoryRepo(MakanyApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IndexCategory>> GetDetailsAsync(int Id)
        {
            try
            {
                return new CommitResult<IndexCategory>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Category>().ProjectToType<IndexCategory>().SingleOrDefaultAsync(category => category.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<IndexCategory>
                {
                    IsSuccess = false,
                    ErrorCode = "CAT-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexCategory>>> GetAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<IndexCategory>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Category>().Where(category => !category.IsDeleted).ProjectToType<IndexCategory>().ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexCategory>>
                {
                    IsSuccess = false,
                    ErrorCode = "CAT-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<UpdateCategory>> GetCategoryForUpdateAsync(int Id)
        {
            try
            {
                return new CommitResult<UpdateCategory>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Category>().ProjectToType<UpdateCategory>().SingleOrDefaultAsync(category => category.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<UpdateCategory>
                {
                    IsSuccess = false,
                    ErrorCode = "CAT-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<string>> CreateAsync(CreateCategory model)
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
                            ErrorCode = "CAT-X0002",
                            ErrorType = ErrorType.Error
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Name))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "CAT-X0003",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Description))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "CAT-X0004",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    Category tempCategory = await _dbContext.Set<Category>().Where(category => category.Name.Equals(model.Name) && category.Description.Equals(model.Description) && !category.IsDeleted).SingleOrDefaultAsync();

                    if (tempCategory != null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "CAT-X0005",
                            ErrorType = ErrorType.Error
                        };
                    }

                    tempCategory = new Category
                    {
                        Name = model.Name,
                        Description = model.Description,
                        IsDeleted = false
                    };

                    _dbContext.Set<Category>().Add(tempCategory);

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
                        ErrorCode = "CAT-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
            }
        }

        public async Task<CommitResult<string>> UpdateAsync(UpdateCategory model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CAT-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CAT-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Description))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CAT-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                Category tempCategory = await _dbContext.Set<Category>().Where(category => category.Name.Equals(model.Name) && category.Description.Equals(model.Description) && !category.IsDeleted).SingleOrDefaultAsync();

                if (tempCategory != null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CAT-X0005",
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
                    ErrorCode = "CAT-X0001",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
