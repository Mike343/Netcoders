namespace Coders.Tests
{
	public interface IWeapon
	{
		string Name { get; }
	}

	public interface IWarrior
	{
		IWeapon Weapon { get; }
	}

	public class Dagger : IWeapon
	{
		public string Name
		{
			get
			{
				return "Dagger";
			}
		}
	}

	public class Samurai : IWarrior
	{
		public Samurai(IWeapon weapon)
		{
			this.Weapon = weapon;
		}

		public IWeapon Weapon
		{
			get; 
			set;
		}
	}
}