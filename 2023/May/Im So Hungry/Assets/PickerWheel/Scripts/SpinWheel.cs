﻿using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;

public class SpinWheel : MonoBehaviour
{
    [SerializeField] private Button uiSpinButton, uiNotBtn;
    [SerializeField] private Text uiSpinButtonText;

    [SerializeField] private PickerWheel pickerWheel;


    private void Start()
    {
        uiSpinButton.onClick.AddListener(() =>
        {

            uiSpinButton.interactable = false;
            uiNotBtn.interactable = false;
            uiSpinButtonText.text = "Spinning";

            pickerWheel.OnSpinEnd(wheelPiece =>
            {
                // Debug.Log (
                //    @" <b>Index:</b> " + wheelPiece.Index + "           <b>Label:</b> " + wheelPiece.Label
                //    + "\n <b>Amount:</b> " + wheelPiece.Amount + "      <b>Chance:</b> " + wheelPiece.Chance + "%"
                // ) ;

                // uiSpinButton.interactable = true ;
                // uiSpinButtonText.text = "Spin" ;
            });

            pickerWheel.Spin();

        });

    }

}
