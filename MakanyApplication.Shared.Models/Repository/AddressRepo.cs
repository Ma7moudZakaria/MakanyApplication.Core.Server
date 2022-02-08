using MakanyApplication.Shared.Models.DataTransferObjects.Address;
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
    public class AddressRepo
    {
        private readonly MakanyApplicationDbContext _dbContext;

        public AddressRepo(MakanyApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IndexAddress>> GetDetailsAsync(int Id)
        {
            try
            {
                return new CommitResult<IndexAddress>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Address>().ProjectToType<IndexAddress>().SingleOrDefaultAsync(address => address.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<IndexAddress>
                {
                    IsSuccess = false,
                    ErrorCode = "AD-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexAddress>>> GetAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<IndexAddress>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Address>().Where(address => !address.IsDeleted).ProjectToType<IndexAddress>().ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexAddress>>
                {
                    IsSuccess = false,
                    ErrorCode = "AD-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<UpdateAddress>> GetAddressForUpdateAsync(int Id)
        {
            try
            {
                return new CommitResult<UpdateAddress>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Address>().ProjectToType<UpdateAddress>().SingleOrDefaultAsync(address => address.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<UpdateAddress>
                {
                    IsSuccess = false,
                    ErrorCode = "AD-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<string>> CreateAsync(CreateAddress model)
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
                            ErrorCode = "AD-X0002",
                            ErrorType = ErrorType.Error
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.BuildingName))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AD-X0003",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.BuildingNumber <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AD-X0004",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.Longitude <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AD-X0005",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.Latitude <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AD-X0006",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.CityId <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AD-X0007",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.CountryId <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AD-X0008",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.GovernmentId <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AD-X0009",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.ItemId <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AD-X00010",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.AreaId <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AD-X00011",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.UserId <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AD-X00012",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    Address tempAddress = await _dbContext.Set<Address>().Where(address => address.UserId.Equals(model.UserId) && address.ItemId.Equals(model.ItemId) && !address.IsDeleted).SingleOrDefaultAsync();

                    if (tempAddress != null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AD-X00013",
                            ErrorType = ErrorType.Error
                        };
                    }

                    tempAddress = new Address
                    {
                        UserId = model.UserId , AreaId = model.AreaId , ItemId = model.ItemId , GovernmentId = model.GovernmentId , CountryId = model.CountryId , CityId = model.CityId , Latitude = model.Latitude , 
                        BuildingName = model.BuildingName , BuildingNumber = model.BuildingNumber , Longitude = model.Longitude , IsDeleted = false
                    };

                    _dbContext.Set<Address>().Add(tempAddress);

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
                        ErrorCode = "AD-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
            }
        }

        public async Task<CommitResult<string>> UpdateAsync(UpdateAddress model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.BuildingName))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.BuildingNumber <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.Longitude <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0005",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.Latitude <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0006",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.CityId <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0007",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.CountryId <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0008",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.GovernmentId <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0009",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.ItemId <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X00010",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.AreaId <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X00011",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.UserId <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X00012",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                Address tempAddress = await _dbContext.Set<Address>().Where(address => address.UserId.Equals(model.UserId) && address.ItemId.Equals(model.ItemId) && !address.IsDeleted).SingleOrDefaultAsync();

                if (tempAddress != null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X00013",
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
                    ErrorCode = "AD-X0001",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
