using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAL
{
    public class jsonData : MonoBehaviour
    {

        private string _JsonString = "{\"playerId\":\"8484239823\",\"playerLoc\":\"Powai\",\"playerNick\":\"Random Nick\"}";

        [Serializable]
        public class Player
        {
            public string playerId;
            public string playerLoc;
            public string playerNick;
        }


        Player playerInstance = new Player();

        private void Start()
        {
            Serialize();
            Deserialize();
            SerializeArray();
        }



        private void Serialize()
        {
            playerInstance.playerId = "8484239823";
            playerInstance.playerLoc = "Powai";
            playerInstance.playerNick = "Random Nick";

            //Convert to JSON
            string playerToJson = JsonUtility.ToJson(playerInstance);
            Debug.Log(playerToJson);

        }

        private void Deserialize()
        {
            //Overwrite the values in the existing class instance "playerInstance". Less memory Allocation
            JsonUtility.FromJsonOverwrite(_JsonString, playerInstance);
            Debug.Log(playerInstance.playerLoc);
        }

        private void SerializeArray()
        {
            Player[] playerInstance = new Player[2];

            playerInstance[0] = new Player();
            playerInstance[0].playerId = "8484239823";
            playerInstance[0].playerLoc = "Powai";
            playerInstance[0].playerNick = "Random Nick";

            playerInstance[1] = new Player();
            playerInstance[1].playerId = "512343283";
            playerInstance[1].playerLoc = "User2";
            playerInstance[1].playerNick = "Rand Nick 2";

            //Convert to JSON
            string playerToJson = JsonHelper.ToJson(playerInstance, true);
            Debug.Log(playerToJson);
        }

        private void DeserializeArray() // V1
        {
            string jsonString = "{\r\n    \"Items\": [\r\n        {\r\n            \"playerId\": \"8484239823\",\r\n            \"playerLoc\": \"Powai\",\r\n            \"playerNick\": \"Random Nick\"\r\n        },\r\n        {\r\n            \"playerId\": \"512343283\",\r\n            \"playerLoc\": \"User2\",\r\n            \"playerNick\": \"Rand Nick 2\"\r\n        }\r\n    ]\r\n}";

            Player[] player = JsonHelper.FromJson<Player>(jsonString);
            Debug.Log(player[0].playerLoc);
            Debug.Log(player[1].playerLoc);

        }
        string fixJson(string value)
        {
            value = "{\"Items\":" + value + "}";
            return value;
        }
    }
}


//////////////////////// http://wiki.unity3d.com/index.php/SimpleJSON
//////////////////////// http://json2csharp.com/

//Unity added JsonUtility to their API after 5.3.3 Update.Forget about all the 3rd party libraries unless you are doing something more complicated.JsonUtility is faster than other Json libraries.Update to Unity 5.3.3 version or above then try the solution below.

//JsonUtility is a lightweight API.Only simple types are supported.It does not support collections such as Dictionary.One exception is List.It supports List and List array!


//If you need to serialize a Dictionary or do something other than simply serializing and deserializing simple datatypes, use a third-party API. Otherwise, continue reading.

//Example class to serialize:

//[Serializable]
//public class Player
//{
//    public string playerId;
//    public string playerLoc;
//    public string playerNick;
//}
//1. ONE DATA OBJECT(NON-ARRAY JSON)

//Serializing Part A:

//Serialize to Json with the public static string ToJson(object obj); method.

//Player playerInstance = new Player();
//playerInstance.playerId = "8484239823";
//playerInstance.playerLoc = "Powai";
//playerInstance.playerNick = "Random Nick";

////Convert to JSON
//string playerToJson = JsonUtility.ToJson(playerInstance);
//Debug.Log(playerToJson);
//Output:

//{"playerId":"8484239823","playerLoc":"Powai","playerNick":"Random Nick"}
//Serializing Part B:

//Serialize to Json with the public static string ToJson(object obj, bool prettyPrint); method overload.Simply passing true to the JsonUtility.ToJson function will format the data. Compare the output below to the output above.

//Player playerInstance = new Player();
//playerInstance.playerId = "8484239823";
//playerInstance.playerLoc = "Powai";
//playerInstance.playerNick = "Random Nick";

////Convert to JSON
//string playerToJson = JsonUtility.ToJson(playerInstance, true);
//Debug.Log(playerToJson);
//Output:

//{
//    "playerId": "8484239823",
//    "playerLoc": "Powai",
//    "playerNick": "Random Nick"
//}
//Deserializing Part A:

//Deserialize json with the public static T FromJson(string json); method overload.

//string jsonString = "{\"playerId\":\"8484239823\",\"playerLoc\":\"Powai\",\"playerNick\":\"Random Nick\"}";
//Player player = JsonUtility.FromJson<Player>(jsonString);
//Debug.Log(player.playerLoc);
//Deserializing Part B:

//Deserialize json with the public static object FromJson(string json, Type type); method overload.

//string jsonString = "{\"playerId\":\"8484239823\",\"playerLoc\":\"Powai\",\"playerNick\":\"Random Nick\"}";
//Player player = (Player)JsonUtility.FromJson(jsonString, typeof(Player));
//Debug.Log(player.playerLoc);
//Deserializing Part C:

//Deserialize json with the public static void FromJsonOverwrite(string json, object objectToOverwrite); method.When JsonUtility.FromJsonOverwrite is used, no new instance of that Object you are deserializing to will be created.It will simply re-use the instance you pass in and overwrite its values.

//This is efficient and should be used if possible.

//Player playerInstance;
//void Start()
//{
//    //Must create instance once
//    playerInstance = new Player();
//    deserialize();
//}

//void deserialize()
//{
//    string jsonString = "{\"playerId\":\"8484239823\",\"playerLoc\":\"Powai\",\"playerNick\":\"Random Nick\"}";

//    //Overwrite the values in the existing class instance "playerInstance". Less memory Allocation
//    JsonUtility.FromJsonOverwrite(jsonString, playerInstance);
//    Debug.Log(playerInstance.playerLoc);
//}
//2. MULTIPLE DATA(ARRAY JSON)

//Your Json contains multiple data objects.For example playerId appeared more than once. Unity's JsonUtility does not support array as it is still new but you can use a helper class from this person to get array working with JsonUtility.

//Create a class called JsonHelper.Copy the JsonHelper directly from below.

//public static class JsonHelper
//{
//    public static T[] FromJson<T>(string json)
//    {
//        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
//        return wrapper.Items;
//    }

//    public static string ToJson<T>(T[] array)
//    {
//        Wrapper<T> wrapper = new Wrapper<T>();
//        wrapper.Items = array;
//        return JsonUtility.ToJson(wrapper);
//    }

//    public static string ToJson<T>(T[] array, bool prettyPrint)
//    {
//        Wrapper<T> wrapper = new Wrapper<T>();
//        wrapper.Items = array;
//        return JsonUtility.ToJson(wrapper, prettyPrint);
//    }

//    [Serializable]
//    private class Wrapper<T>
//    {
//        public T[] Items;
//    }
//}
//Serializing Json Array:

//Player[] playerInstance = new Player[2];

//playerInstance[0] = new Player();
//playerInstance[0].playerId = "8484239823";
//playerInstance[0].playerLoc = "Powai";
//playerInstance[0].playerNick = "Random Nick";

//playerInstance[1] = new Player();
//playerInstance[1].playerId = "512343283";
//playerInstance[1].playerLoc = "User2";
//playerInstance[1].playerNick = "Rand Nick 2";

////Convert to JSON
//string playerToJson = JsonHelper.ToJson(playerInstance, true);
//Debug.Log(playerToJson);
//Output:

//{
//    "Items": [
//        {
//            "playerId": "8484239823",
//            "playerLoc": "Powai",
//            "playerNick": "Random Nick"
//        },
//        {
//            "playerId": "512343283",
//            "playerLoc": "User2",
//            "playerNick": "Rand Nick 2"
//        }
//    ]
//}
//Deserializing Json Array:

//string jsonString = "{\r\n    \"Items\": [\r\n        {\r\n            \"playerId\": \"8484239823\",\r\n            \"playerLoc\": \"Powai\",\r\n            \"playerNick\": \"Random Nick\"\r\n        },\r\n        {\r\n            \"playerId\": \"512343283\",\r\n            \"playerLoc\": \"User2\",\r\n            \"playerNick\": \"Rand Nick 2\"\r\n        }\r\n    ]\r\n}";

//Player[] player = JsonHelper.FromJson<Player>(jsonString);
//Debug.Log(player[0].playerLoc);
//Debug.Log(player[1].playerLoc);
//Output:

//Powai

//User2

//If this is a Json array from the server and you did not create it by hand:

//You may have to Add {"Items": in front of the received string then add }
//at the end of it.

//I made a simple function for this:

//string fixJson(string value)
//{

//value = "{\"Items\":" + value + "}";
//    return value;
//}
//then you can use it:

//string jsonString = fixJson(yourJsonFromServer);
//Player[] player = JsonHelper.FromJson<Player>(jsonString);
//3.Deserialize json string without class && De-serializing Json with numeric properties

//This is a Json that starts with a number or numeric properties.

//For example:

//{
//    "USD" : { "15m" : 1740.01, "last" : 1740.01, "buy" : 1740.01, "sell" : 1744.74, "symbol" : "$"}, 

//"ISK" : { "15m" : 179479.11, "last" : 179479.11, "buy" : 179479.11, "sell" : 179967, "symbol" : "kr"},

//"NZD" : { "15m" : 2522.84, "last" : 2522.84, "buy" : 2522.84, "sell" : 2529.69, "symbol" : "$"}
//}
//Unity's JsonUtility does not support this because the "15m" property starts with a number. A class variable cannot start with an integer.

//Download SimpleJSON.cs from Unity's wiki.

//To get the "15m" property of USD:

//var N = JSON.Parse(yourJsonString);
//string price = N["USD"]["15m"].Value;
//Debug.Log(price);
//To get the "15m" property of ISK:

//var N = JSON.Parse(yourJsonString);
//string price = N["ISK"]["15m"].Value;
//Debug.Log(price);
//To get the "15m" property of NZD:

//var N = JSON.Parse(yourJsonString);
//string price = N["NZD"]["15m"].Value;
//Debug.Log(price);
//The rest of the Json properties that doesn't start with a numeric digit can be handled by Unity's JsonUtility.

//4.TROUBLESHOOTING JsonUtility:

//Problems when serializing with JsonUtility.ToJson?

//Getting empty string or "{}" with JsonUtility.ToJson?

//A. Make sure that the class is not an array.If it is, use the helper class above with JsonHelper.ToJson instead of JsonUtility.ToJson.

//B.Add[Serializable] to the top of the class you are serializing.

//C.Remove property from the class. For example, in the variable, public string playerId { get; set; }
//remove { get; set; }. Unity cannot serialize this.

//Problems when deserializing with JsonUtility.FromJson?

//A. If you get Null, make sure that the Json is not a Json array. If it is, use the helper class above with JsonHelper.FromJson instead of JsonUtility.FromJson.

//B.If you get NullReferenceException while deserializing, add[Serializable] to the top of the class.

//C.Any other problems, verify that your json is valid.Go to this site here and paste the json. It should show you if the json is valid.It should also generate the proper class with the Json.Just make sure to remove remove { get; set; }
//from each variable and also add[Serializable] to the top of each class generated.

//Newtonsoft.Json:

//If for some reason Newtonsoft.Json must be used then check out the forked version for Unity here.Note that you may experience crash if certain feature is used.Be careful.

//To answer your question:

//Your original data is

// [{ "playerId":"1","playerLoc":"Powai"},{"playerId":"2","playerLoc":"Andheri"},{"playerId":"3","playerLoc":"Churchgate"}]
//Add {"Items": in front of it then add } at the end of it.

//Code to do this:

//serviceData = "{\"Items\":" + serviceData + "}";
//Now you have:

// {"Items":[{"playerId":"1","playerLoc":"Powai"},{"playerId":"2","playerLoc":"Andheri"},{"playerId":"3","playerLoc":"Churchgate"}]}
//To serialize the multiple data from php as arrays, you can now do

//public player[] playerInstance;
//playerInstance = JsonHelper.FromJson<player>(serviceData);
//playerInstance[0] is your first data

//playerInstance[1] is your second data

//playerInstance[2] is your third data

//or data inside the class with playerInstance[0].playerLoc, playerInstance[1].playerLoc, playerInstance[2].playerLoc ......

//You can use playerInstance.Length to check the length before accessing it.

//NOTE: Remove { get; set; }
//from the player class. If you have { get; set; }, it won't work. Unity's JsonUtility does NOT work with class members that are defined as properties.