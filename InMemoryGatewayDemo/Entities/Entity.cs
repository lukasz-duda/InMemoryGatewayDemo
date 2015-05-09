namespace InMemoryGatewayDemo.Entities
{
    public abstract class Entity
    {
        public virtual long Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var e = obj as Entity;
            if ((object)e == null)
            {
                return false;
            }

            return this == e;
        }

        public static bool operator ==(Entity x, Entity y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if ((object)x == null || (object)y == null)
            {
                return false;
            }

            return x.Id == y.Id;
        }

        public static bool operator !=(Entity x, Entity y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
