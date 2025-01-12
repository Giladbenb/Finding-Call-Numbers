using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog3B
{
    //DeweyDecimalClass 
  public class DeweyDecimalClass
    {

    
        // Desc and CallNumbes getter and setter 
public string callDesc { get; set; }

public string callNum { get; set; }

public DeweyDecimalClass(string callNum, string callDesc)

{

    this.callDesc = callDesc;

    this.callNum = callNum;

}

public override string ToString()

{

    string re = $"{this.callNum} {this.callDesc}";

    return re;

}

protected bool Equals(DeweyDecimalClass obj)

{

    return callDesc == obj.callDesc;

}

public override bool Equals(object? obj)

{

    if (ReferenceEquals(null, obj)) return false;

    if (ReferenceEquals(this, obj)) return true;

    if (obj.GetType() != GetType()) return false;

    return Equals((DeweyDecimalClass)obj);

}

public override int GetHashCode()

{

    return base.GetHashCode();

}

    }

}