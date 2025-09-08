using TMPro;
using UnityEngine;

public struct SignupData
{
    public string username;
    public string password;
    public string nickname;
}

public struct SignupResult
{
    public int result;
}

public class SignupPanelController : PanelController
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField confirmPasswordInputField;
    [SerializeField] private TMP_InputField nicknameInputField;

    public void OnClickConfirmButton()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string confirmPassword = confirmPasswordInputField.text;
        string nickname = nicknameInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
            string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(nickname))
        {
            Shake();
            return;
        }

        if (password.Equals(confirmPassword))
        {
            var signupData = new SignupData();
            signupData.username = username;
            signupData.password = password;
            signupData.nickname = nickname;

            StartCoroutine(NetworkManager.Instance.Signup(signupData,
            () =>
            {
                GameManager.Instance.OpenConfirmPanel("회원가입에 성공했습니다.",
                    () =>
                    {
                        Hide();
                    });
            },
            (result) =>
            {
                if (result == 0)
                {
                    GameManager.Instance.OpenConfirmPanel("이미 존재하는 사용자입니다.",
                        () =>
                        {
                            usernameInputField.text = "";
                            passwordInputField.text = "";
                            confirmPasswordInputField.text = "";
                            nicknameInputField.text = "";
                        });
                }
                else if (result == 1)
                {
                    GameManager.Instance.OpenConfirmPanel("패스워드가 유효하지 않습니다.",
                        () =>
                        {
                            passwordInputField.text = "";
                        });
                }
            }));

        }
        else
        {

        }
    }

    public void OnClickCancelButton()
    {
        Hide();
    }
}
