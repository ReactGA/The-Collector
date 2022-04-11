using UnityEngine;
using UnityEngine.SceneManagement;

public class PubSceneManager : MonoBehaviour, IAttachment
{
    [SerializeField] GameObject FadeCanvas;
    private void Awake()
    {
        FadeCanvas.GetComponent<Animator>().Play("FadeIn");
    }
    void Start()
    {
        CustomInvoke.i.Invoke(1.5f, () => { FadeCanvas.SetActive(false); });
        CustomInvoke.i.Invoke(2.5f, () => { InstructionClass.options.ShowInstruction(0, 1); });
    }
    public void AttachmentCallNext(int index) { }
    public void AttachmentCallCont(int index)
    {
        if (index == 1)
        {
            FadeCanvas.SetActive(true);
            FadeCanvas.GetComponent<Animator>().Play("FadeOut");
            CustomInvoke.i.Invoke(1.5f, () => { SceneManager.LoadScene(2); });
        }
    }
}
