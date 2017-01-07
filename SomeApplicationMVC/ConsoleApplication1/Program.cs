using System;

class Program
{
    static int Main(string[] args)
    {
        ITransport transport = new Auto();
        Driver driver = new Driver(transport);
        driver.Drive();
        

        return 0;
    }
}

public interface ITransport
{
    void Drive();
}
public interface IAnimal
{
    void Move();
}

class Camel : IAnimal
{
    public void Move()
    {
        throw new NotImplementedException();
    }
}

class Auto : ITransport
{
    public void Drive()
    {
        throw new NotImplementedException();
    }
}

class Driver
{
    ITransport transport;
    public Driver(ITransport transport)
    {
        this.transport = transport;
    }

    public void Drive()
    {
        transport.Drive();
    }
}

class AnimalToTransportAdapter : ITransport
{
    IAnimal animal;
    public AnimalToTransportAdapter(IAnimal animal)
    {
        this.animal = animal;
    }
    public void Drive()
    {
        animal.Move();
    }
}