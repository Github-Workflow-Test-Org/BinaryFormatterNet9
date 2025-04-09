using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

var taint = string.Empty;
if (args.Length>1)
{
    taint = args[1]; //Propagates taint. 
}

//BinaryFormatter is obsolete, with package reference + MsBuildFlag `EnableUnsafeBinaryFormatterSerialization` you're still able to use it.
//Type itself moved to different package that's why we need to work on getting the new signature in risky functions.

var mem = new MemoryStream(Encoding.UTF8.GetBytes(taint));
var form = new BinaryFormatter();
var createdFromTaintedStream = form.Deserialize(mem); //CWEID 501

var mem2 = new MemoryStream(Encoding.UTF8.GetBytes("ThisIsStaticText"));
var createdFromStaticStringNoIssue = form.Deserialize(mem); //No issue