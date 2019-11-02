﻿using UnityEngine;

public class GameBGControl : BackgroundControl {

    private PanelControl panelKontrol;
    private SpawnKontrol spawnKontrol;

    private new void Start()
    {
        base.Start();
        findObjects();
    }

    private void findObjects()
    {
        panelKontrol = GameObject.FindGameObjectWithTag("panelTag").GetComponent<PanelControl>();
        spawnKontrol = GameObject.FindGameObjectWithTag("spawnPointTag").GetComponent<SpawnKontrol>();
        assignCycleSeconds();
    }

    private void assignCycleSeconds()
    {
        panelKontrol.cycleSeconds = cycleSeconds;
        spawnKontrol.cycleSeconds = cycleSeconds;
    }

    public override void changeBGColor()
    {
        base.changeBGColor();
        changeObjectColor();
    }

    private void changeObjectColor()
    {
        panelKontrol.panelChangeColor(colorRateR, colorRateG, colorRateB); //arkaplan ile birlikte panelin de rengini degistir
        spawnKontrol.sekilRenkDegistir(colorRateR, colorRateG, colorRateB); //arkaplan ile square'in de rengini degistir
    }
}
