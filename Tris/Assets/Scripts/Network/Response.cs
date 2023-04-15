using UnityEngine;

[System.Serializable]
public class Response
{
    public string status;
    public string message;

    public virtual void Print()
    {
        Debug.Log("status: " + status);
        Debug.Log("message: " + message);
    }
}

[System.Serializable]
public class _DataNext
{
    public int nextMove;
}

[System.Serializable]
public class _DataResult
{
    public string result;
}

[System.Serializable]
public class NextMove:Response
{
    public _DataNext data;
    public override void Print()
    {
        base.Print();
        Debug.Log("data (nextMove): " + data.nextMove);
    }
}

[System.Serializable]
public class Result : Response
{
    public _DataResult data;

    public override void Print()
    {
        base.Print();
        Debug.Log("data (result): " + data.result);
    }
}

