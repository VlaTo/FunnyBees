namespace LibraProgramming.FunnyBees.Interop
{
    public interface IEntity
    {
        object this[EntityProperty property]
        {
            get;
            set;
        }

        bool PropertyExists(EntityProperty property);
    }
}