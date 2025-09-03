using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public void OnClickBackButton()
    {
        //GameManager.Instance.ChangeToMainScene();

        GameManager.Instance.OpenConfirmPanel(message:"게임을 종료하시겠습니까?",
            () =>
            {
                GameManager.Instance.ChangeToMainScene();
            });
    }
}
