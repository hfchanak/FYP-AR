//using UdpKit;
//using System;

//public class ServerAcceptToken : Bolt.IProtocolToken
//{
//    public String data;

//    public void Read(UdpPacket packet)
//    {
//        data = packet.ReadString();
//    }

//    public void Write(UdpPacket packet)
//    {
//        packet.WriteString(data);
//    }
//}

//public class ServerConnectToken : Bolt.IProtocolToken
//{
//    public String data;

//    public void Read(UdpPacket packet)
//    {
//        data = packet.ReadString();
//    }

//    public void Write(UdpPacket packet)
//    {
//        packet.WriteString(data);
//    }
//}