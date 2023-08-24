using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillAmountController : MonoBehaviour
{
    public Image fillImage; // FillAmount�� ������ �̹���
    public Animator animator; // �ִϸ����� ������Ʈ
    public TextMeshProUGUI[] text;

    private float originalFillAmount; // �ʱ� FillAmount ��
    private float animationClipLength; // �ִϸ��̼� Ŭ���� ��� �ð�
    private bool isAnimating; // �ִϸ��̼� ��� ����

    private int previousNumber = 0;

    private void Start()
    {
        originalFillAmount = fillImage.fillAmount;
    }

    private void Update()
    {
        if (isAnimating)
        {
            float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float fillAmount = Mathf.Lerp(0f, 1f, normalizedTime);
            fillImage.fillAmount = fillAmount;

            if (normalizedTime >= 1f)
            {
                isAnimating = false;
                fillImage.fillAmount = 0;
                GetComponent<AnimPlayer>().buttonReset();
            }
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ���� �Լ�
    public void OnAnimationEvent()
    {
        // ���� ��� ���� �ִϸ��̼� Ŭ���� ���̸� ������
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        AnimatorClipInfo[] currentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (currentClipInfo.Length > 0)
        {
            foreach (AnimationClip clip in clips)
            {
                if (clip.name == currentClipInfo[0].clip.name)
                {
                    animationClipLength = clip.length; 
                    break;
                }
            }
        }

        // FillAmount ���� ����
        isAnimating = true;
    }

    public void highlightingText(int i)
    {
        if(i == 3 && previousNumber == 3)
        {
            return;
        }
        //Debug.Log($"i : {i}, previous : {previousNumber}");
        text[i].color = Color.white;
        text[i].fontStyle = FontStyles.Bold;
        text[previousNumber].color = Color.gray;
        text[previousNumber].fontStyle = FontStyles.Normal;
        previousNumber = i;
    }
}