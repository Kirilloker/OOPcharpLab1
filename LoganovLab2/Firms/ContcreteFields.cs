using LoganovLab1.Domain;
using LoganovLab2.Rules;

namespace LoganovLab2.Firms
{
    public class NameField : Field
    {
        public NameField() : base("Name") { }
        public override string GetValue(Firm firm) => firm.FullName ?? "";
        public override Field Clone() => new NameField();
        public override FilterRule CreateRule() => new NameRule(this);
        public override FieldDataType GetFieldDataType() => FieldDataType.String;
    }

    public class RegionField : Field
    {
        public RegionField() : base("Region") { }
        public override string GetValue(Firm firm) => firm.Region ?? "";
        public override Field Clone() => new RegionField();
        public override FilterRule CreateRule() => new RegionRule(this);
        public override FieldDataType GetFieldDataType() => FieldDataType.String;
    }

    public class TownField : Field
    {
        public TownField() : base("Town") { }
        public override string GetValue(Firm firm) => firm.City ?? "";
        public override Field Clone() => new TownField();
        public override FilterRule CreateRule() => new TownRule(this);
        public override FieldDataType GetFieldDataType() => FieldDataType.String;
    }

    public class PostInxField : Field
    {
        public PostInxField() : base("PostInx") { }
        public override string GetValue(Firm firm)
        {
            return firm.GetUserFieldValue("Field1") ?? "";
        }
        public override Field Clone() => new PostInxField();
        public override FilterRule CreateRule() => new PostInxRule(this);
        public override FieldDataType GetFieldDataType() => FieldDataType.String;
    }

    public class DateInField : Field
    {
        public DateInField() : base("DateIn") { }
        public override string GetValue(Firm firm)
        {
            return firm.GetUserFieldValue("Field2") ?? "";
        }
        public override Field Clone() => new DateInField();
        public override FilterRule CreateRule() => new DateInRule(this);
        public override FieldDataType GetFieldDataType() => FieldDataType.DateTime;
    }

    public class DateBegField : Field
    {
        public DateBegField() : base("DateBeg") { }
        public override string GetValue(Firm firm)
        {
            return firm.InsertDate?.ToString("dd.MM.yyyy") ?? "";
        }
        public override Field Clone() => new DateBegField();
        public override FilterRule CreateRule() => new DateBegRule(this);
        public override FieldDataType GetFieldDataType() => FieldDataType.DateTime;
    }

    public class WebField : Field
    {
        public WebField() : base("Web") { }
        public override string GetValue(Firm firm) => firm.Website ?? "";
        public override Field Clone() => new WebField();
        public override FilterRule CreateRule() => new WebRule(this);
        public override FieldDataType GetFieldDataType() => FieldDataType.String;
    }

    public class CountryField : Field
    {
        public CountryField() : base("Country") { }
        public override string GetValue(Firm firm)
        {
            return firm.GetUserFieldValue("Field3") ?? "";
        }
        public override Field Clone() => new CountryField();
        public override FilterRule CreateRule() => new CountryRule(this);
        public override FieldDataType GetFieldDataType() => FieldDataType.String;
    }

    public class CountContField : Field
    {
        public CountContField() : base("CountCont") { }
        public override string GetValue(Firm firm)
        {
            var contacts = firm.GetAllContacts();
            return contacts.Length.ToString();
        }
        public override Field Clone() => new CountContField();
        public override FilterRule CreateRule() => new ContCountRule(this);
        public override FieldDataType GetFieldDataType() => FieldDataType.Int;
    }

    public class UsrField : Field
    {
        public UsrField() : base("Usr") { }
        public override string GetValue(Firm firm)
        {
            return firm.GetUserFieldValue("Field4") ?? "";
        }
        public override Field Clone() => new UsrField();
        public override FilterRule CreateRule() => new UsrFieldRule(this);
        public override FieldDataType GetFieldDataType() => FieldDataType.String;
    }
}
