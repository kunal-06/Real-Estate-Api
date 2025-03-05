using FluentValidation;
using Microsoft.Identity.Client;
using Property_API.Model;
using System.Diagnostics.Metrics;

namespace Property_API.Validation
{
    public class UserValidatro : AbstractValidator<UserModel>
    {
        public UserValidatro()
        {
           // RuleFor(user => user.UserID)
           //.GreaterThan(0).WithMessage("UserID must be greater than 0.");

            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .Length(3, 50).WithMessage("UserName must be between 3 and 50 characters.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one digit.")
                .Matches(@"[@$!%*?&#]").WithMessage("Password must contain at least one special character.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\d{10}$").WithMessage("Phone must be a valid 10-digit number.");

            RuleFor(user => user.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => role == "Admin" || role == "Buyer" || role == "Seller")
                .WithMessage("Role must be 'Admin', 'Buyer', or 'Seller'.");

            RuleFor(user => user.Created)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Created date cannot be in the future.");
        }
    }

    public class CityValidator : AbstractValidator<CityModel>
    {
        public CityValidator()
        {
            //RuleFor(city => city.CityID)
            //    .GreaterThan(0).WithMessage("CityID must be greater than 0.");

            RuleFor(city => city.CityName)
                .NotEmpty().WithMessage("CityName is required.")
                .Length(3, 100).WithMessage("CityName must be between 3 and 100 characters.");

            RuleFor(city => city.StateID)
                .GreaterThan(0).WithMessage("StateID must be greater than 0.");

            RuleFor(city => city.Created)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Created date cannot be in the future.");
        }
    }
    public class StateValidator : AbstractValidator<StateModel>
    {
        public StateValidator()
        {
            //RuleFor(state => state.StateID)
            //    .GreaterThan(0).WithMessage("StateID must be greater than 0.");

            RuleFor(state => state.StateName)
                .NotEmpty().WithMessage("StateName is required.")
                .Length(3, 100).WithMessage("StateName must be between 3 and 100 characters.");

            RuleFor(state => state.CountryID)
                .GreaterThan(0).WithMessage("CountryID must be greater than 0.");

            RuleFor(state => state.Created)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Created date cannot be in the future.");
        }
    }

    public class CountryValidator : AbstractValidator<CountryModel>
    {
        public CountryValidator()
        {
            //RuleFor(country => country.CountryID)
            //    .GreaterThan(0).WithMessage("CountryID must be greater than 0.");

            RuleFor(country => country.CountryName)
                .NotEmpty().WithMessage("CountryName is required.")
                .Length(3, 100).WithMessage("CountryName must be between 3 and 100 characters.");

            RuleFor(country => country.Created)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Created date cannot be in the future.");
        }
    }

    public class PropertyValidator : AbstractValidator<PropertyModel>
    {
        public PropertyValidator()
        {
            //RuleFor(property => property.PropertyID)
            //    .GreaterThan(0).WithMessage("PropertyID must be greater than 0.");

            RuleFor(property => property.PropertyName)
                .NotEmpty().WithMessage("PropertyName is required.")
                .Length(3, 100).WithMessage("PropertyName must be between 3 and 100 characters.");

            RuleFor(property => property.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(3, 100).WithMessage("Title must be between 3 and 100 characters.");

            RuleFor(property => property.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(property => property.PropertyType)
                .NotEmpty().WithMessage("PropertyType is required.")
                .Must(type => type == "For Rent" || type == "For Sell")
                .WithMessage("Status must be 'Rent', or 'Sell'."); ;

            RuleFor(property => property.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");


            RuleFor(property => property.CityID)
                .GreaterThan(0).WithMessage("CityID must be greater than 0.");

            RuleFor(property => property.Price)
                .NotEmpty().WithMessage("Price is required.");
            //.Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Price must be a valid decimal number.");

            RuleFor(property => property.Price_per_sqft)
                .NotEmpty().WithMessage("Price per square foot is required.");
                //.Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Price per square foot must be a valid decimal number.");

            RuleFor(property => property.CarpetArea)
                .GreaterThan(0).WithMessage("CarpetArea must be greater than 0.");

            RuleFor(property => property.BadroomNo)
                .GreaterThan(0).WithMessage("BadroomNo must be greater than 0.");


            //RuleFor(property => property.Listing_Date)
            //    .LessThanOrEqualTo(DateTime.Now).WithMessage("Listing date cannot be in the future.");

            RuleFor(property => property.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => status == "Available" || status == "Inactive" || status == "Sold")
                .WithMessage("Status must be 'Available', 'Inactive', or 'Sold'.");

            RuleFor(property => property.SellerID)
                .GreaterThan(0).WithMessage("SellerID must be greater than 0.");
        }
    }

    public class PropertyUploadValidator : AbstractValidator<PropertyUploadModel>
    {
        public PropertyUploadValidator()
        {
            //RuleFor(property => property.PropertyID)
            //    .GreaterThan(0).WithMessage("PropertyID must be greater than 0.");

            RuleFor(property => property.PropertyName)
                .NotEmpty().WithMessage("PropertyName is required.")
                .Length(3, 100).WithMessage("PropertyName must be between 3 and 100 characters.");

            RuleFor(property => property.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(3, 100).WithMessage("Title must be between 3 and 100 characters.");

            RuleFor(property => property.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(property => property.PropertyType)
                .NotEmpty().WithMessage("PropertyType is required.");

            RuleFor(property => property.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

            

            RuleFor(property => property.CityID)
                .GreaterThan(0).WithMessage("CityID must be greater than 0.");

            RuleFor(property => property.Price)
                .NotEmpty().WithMessage("Price is required.");
            //.Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Price must be a valid decimal number.");

            RuleFor(property => property.Price_per_sqft)
                .NotEmpty().WithMessage("Price per square foot is required.");
            //.Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Price per square foot must be a valid decimal number.");

            RuleFor(property => property.CarpetArea)
                .GreaterThan(0).WithMessage("CarpetArea must be greater than 0.");

            RuleFor(property => property.BadroomNo)
                .GreaterThan(0).WithMessage("BadroomNo must be greater than 0.");

            

            //RuleFor(property => property.Listing_Date)
            //    .LessThanOrEqualTo(DateTime.Now).WithMessage("Listing date cannot be in the future.");

            RuleFor(property => property.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => status == "Active" || status == "Available" || status == "Sold")
                .WithMessage("Status must be 'Active', 'Inactive', or 'Sold'.");

            RuleFor(property => property.SellerID)
                .GreaterThan(0).WithMessage("SellerID must be greater than 0.");
        }
    }
    public class PropertyImgValidator : AbstractValidator<Property_Img_Model>
    {
        public PropertyImgValidator()
        {
            RuleFor(image => image.ImagePath)
                .NotEmpty().WithMessage("Image URL is required.")
                .WithMessage("Image URL must be a valid URL.");

            RuleFor(image => image.PropertyID)
                .GreaterThan(0).WithMessage("PropertyID must be greater than 0.");

            RuleFor(image => image.Created)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Created date cannot be in the future.");
        }
    }

    public class RequestValidator : AbstractValidator<RequestModel>
    {
        public RequestValidator()
        {
            //RuleFor(request => request.RequestID)
            //    .GreaterThan(0).WithMessage("RequestID must be greater than 0.");

            RuleFor(req => req.Status).Must(status => status == "Pending" || status == "Accepted" || status == "Rejected")
                .NotEmpty().WithMessage("Status is required.");

            RuleFor(request => request.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(request => request.BuyerID)
                .GreaterThan(0).WithMessage("BuyerID must be greater than 0.");

            RuleFor(request => request.SellerID)
                .GreaterThan(0).WithMessage("SellerID must be greater than 0.");

            RuleFor(request => request.PropertyID)
                .GreaterThan(0).WithMessage("PropertyID must be greater than 0.");

            RuleFor(request => request.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

            RuleFor(request => request.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone must be a valid phone number.");

            RuleFor(request => request.Created)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Created date cannot be in the future.");
        }
    }
   
    public class VisitBookingValidator : AbstractValidator<Visit_Booking_Model>
    {
        public VisitBookingValidator()
        {
            //RuleFor(booking => booking.BookingID)
            //    .GreaterThan(0).WithMessage("BookingID must be greater than 0.");

            RuleFor(booking => booking.BuyerID)
                .GreaterThan(0).WithMessage("BuyerID must be greater than 0.");

            RuleFor(booking => booking.PropertyID)
                .GreaterThan(0).WithMessage("PropertyID must be greater than 0.");

            RuleFor(booking => booking.Visite_Address)
                .NotEmpty().WithMessage("Visit Address is required.")
                .MaximumLength(200).WithMessage("Visit Address must not exceed 200 characters.");

            RuleFor(booking => booking.Visite_Date)
                .GreaterThan(DateTime.Now).WithMessage("Visit Date must be in the future.");

            RuleFor(booking => booking.Created)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Created date cannot be in the future.");
        }
    }

}