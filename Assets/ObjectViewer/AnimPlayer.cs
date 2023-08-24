using UnityEngine;
using UnityEngine.UI;

public class AnimPlayer : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] ObjToChange;
    [SerializeField] private Material MtlOriginal;
    [SerializeField] private Material MtlTransparent;
    [SerializeField] private Color ColorBlue;
    [SerializeField] private Color ColorOrange;
    [SerializeField] private Light[] m_light;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject cam;

    private Animator anim;
    private Button playButton;
    private Image playImage;

    private Color colorOriginal = Color.white;
    private Color colorDeactived = new Color(255/255f, 255/255f, 255/255f, 13/255f);


    private void Awake()
    {
        anim = GetComponent<Animator>();

        playImage = button.GetComponent<Image>();
        playButton = button.GetComponent<Button>();

    }

    public void animPlay()
    {
        anim.SetTrigger("Trigger");
        playButton.enabled = false;
        playImage.color = colorDeactived;

    }

    public void buttonReset()
    {
        playButton.enabled = true;
        playImage.color = colorOriginal;

    }

    public void TransparentOn()
    {
        for (int i = 0; i < ObjToChange.Length; i++)
        {
            ObjToChange[i].material = MtlTransparent;

            m_light[0].color = ColorOrange;
            m_light[1].color = ColorOrange;
        }
    }

    public void TransparentOff()
    {
        for (int i = 0; i < ObjToChange.Length; i++)
        {
            ObjToChange[i].material = MtlOriginal;

            m_light[0].color = ColorBlue;
            m_light[1].color = ColorBlue;

        }
    }


    public void setDimension(int TrueOrFalse)
    {
        if(TrueOrFalse == 0)
        {
            cam.GetComponent<Simple3DViewer>().ifDimensionShown = true;
        }
        else
        {
            cam.GetComponent<Simple3DViewer>().ifDimensionShown = false;

        }
    }
}
