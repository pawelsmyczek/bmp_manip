using System;

public class Pixel
{
    private byte value;
    private byte red;
    private byte green;
    private byte blue;

    public byte RGBVector;

    public int this[int index]
    {
        get => this[index];
        set => this[index] = value;
    }

    public Pixel()
    {

    }

    public Pixel(byte Value)
    {
        this.Value = Value;
    }
    public Pixel(byte Red, byte Green, byte Blue)
    {
        this.Red = Red;
        this.Green = Green;
        this.Blue = Blue;
    }

    public Pixel(byte Value, byte value) : this(Value)
    {
        this.value = value;
    }

    public Pixel(byte Red, byte Green, byte Blue, byte red, byte green, byte blue) : this(Red, Green, Blue)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
    }

    public static Pixel operator +(Pixel One, Pixel Two)
    {
        byte val1 = One.Value;
        byte val2 = Two.Value;
        int b3 = (int)val1 + (int)val2;
        if (b3 > 255)
        {
            b3 = b3 % 255;
        }
        Pixel Add = new Pixel((byte)b3);
        return Add;
    }

    public static Pixel operator -(Pixel Val, Pixel val)
    {
        byte val1 = Val.Value;
        byte val2 = val.Value;
        int b3 = (int)val1 - (int)val2;
        if(b3 < 0)
        {
            b3 = 255 - (b3 % 255);
        }
        Pixel Add = new Pixel((byte)b3);
        return Add;
    }

    public static Pixel operator *(Pixel Val, Pixel val)
    {
        byte val1 = Val.Value;
        byte val2 = val.Value;
        int b3 = (int)val1 * (int)val2;
        if (b3 > 255)
        {
            b3 = 255;
        }
        Pixel Add = new Pixel((byte)b3);
        return Add;
    }

    public byte Value { get => value; set => this.value = value; }
    public byte Red { get => red; set => red = value; }
    public byte Green { get => green; set => green = value; }
    public byte Blue { get => blue; set => blue = value; }
}