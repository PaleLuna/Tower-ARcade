using UnityEngine;

public class Pony : MonoBehaviour
{
    [SerializeField]
    private int _id;
    [SerializeField]
    private string _name;
    
    public int id => _id;
    public string ponyName => _name;
}
