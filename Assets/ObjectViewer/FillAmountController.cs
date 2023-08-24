using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillAmountController : MonoBehaviour
{
    public Image fillImage; // FillAmount를 조절할 이미지
    public Animator animator; // 애니메이터 컴포넌트
    public TextMeshProUGUI[] text;

    private float originalFillAmount; // 초기 FillAmount 값
    private float animationClipLength; // 애니메이션 클립의 재생 시간
    private bool isAnimating; // 애니메이션 재생 여부

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

    // 애니메이션 이벤트에서 호출할 함수
    public void OnAnimationEvent()
    {
        // 현재 재생 중인 애니메이션 클립의 길이를 가져옴
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

        // FillAmount 조절 시작
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