namespace RedStar.Invoicing.Web._4._6.DataAccess
{
    /// <summary>
    /// A class to easily and clearly indicate that a function or property can return a null value.
    /// </summary>
    public class Optional<T>
    {
        public Optional(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }

        public bool HasValue
        {
            get { return Value != null; }
        }
    }
}
