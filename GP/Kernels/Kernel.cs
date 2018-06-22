namespace GP.Kernels
{
    public abstract class Kernel
    {
        internal abstract double Compute(double left, double right);
    }
}
