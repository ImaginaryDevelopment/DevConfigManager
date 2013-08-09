namespace Domain.Helpers
{
	
	public delegate void VoidHandler();

	/// <summary>
	/// http://codebetter.com/jeremymiller/2007/07/02/build-your-own-cab-12-rein-in-runaway-events-with-the-quot-latch-quot/
	/// </summary>
	public class Latch
	{
		int _count = 0;

		public void Increment() { _count++; }

		public void Decrement() { _count--; }

		public bool IsLatched
		{
			get { return _count > 0; }
		}

		public void RunSingleton(VoidHandler handler)
		{
			if (IsLatched) return;
			this.Increment();
			handler();
			this.Decrement();
		}
		public void RunInsideLatch(VoidHandler handler)
		{
			Increment();
			handler();
			Decrement();
		}

		public void RunLatchedOperation(VoidHandler handler)
		{
			if (IsLatched)
			{
				return;
			}

			handler();
		}
	}
}
