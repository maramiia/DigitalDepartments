using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody[] allRgigdbodies;

    private void Awake()
    {
        for (int i = 0; i < allRgigdbodies.Length; i++)
        {
            allRgigdbodies[i].isKinematic = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakePhysics();
        }
    }

    public void MakePhysics()
    {
        animator.enabled = false;
        for (int i = 0; i < allRgigdbodies.Length; i++)
        {
            allRgigdbodies[i].isKinematic = false;
        }
    }
}
