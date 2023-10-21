using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractHandlerBase : MonoBehaviour
{
    protected abstract string BoardIdentifier {get;}
    protected abstract int numFields {get;}

    protected abstract void ParseMessageData(int[] data);

    // Returns true if the message should not be shown to other
    // handlers.
    public bool ParseMessage(string message) {
        string[] fields = message.Split(',');
        if (fields.Length < 3) {
            // badly formed message
            return true;
        }
        // First field is the id, second is the message type
        if (fields[0] != BoardIdentifier) {
            return false;
        }
        if (fields[1] != "D") {
            // Not a data message, but this is the right handler.
            // TODO: handle data
            return true;
        }
        if (fields.Length < (numFields + 2)) {
            Debug.Log("Failed to parse message: " + message);
        }
        int[] data = new int[numFields];
        for (int i=0; i< numFields; ++i) {
            if (!int.TryParse(fields[i+2], out data[i])) {
                 Debug.Log("Failed to parse " + BoardIdentifier + " Data: " + message);
            }   
        }
        ParseMessageData(data);
        return true;
    }
};
