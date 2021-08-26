using UnityEngine;

public class IntResetter : MonoBehaviour
{
    public IntData totalRooms;
    public ArrayData roomsList;

    private void Start()
    {
        totalRooms.value = 0;
        roomsList.Clear();
    }
}
