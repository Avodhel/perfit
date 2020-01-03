using UnityEngine;

public class GameBGControl : BackgroundControl {

    private PanelControl panelKontrol;
    private SpawnControl spawnKontrol;

    private new void Start()
    {
        base.Start();
        FindObjects();
    }

    private void FindObjects()
    {
        panelKontrol = GameObject.FindGameObjectWithTag("panelTag").GetComponent<PanelControl>();
        spawnKontrol = GameObject.FindGameObjectWithTag("spawnPointTag").GetComponent<SpawnControl>();
        AssignCycleSeconds();
    }

    private void AssignCycleSeconds()
    {
        panelKontrol.cycleSeconds = cycleSeconds;
        spawnKontrol.cycleSeconds = cycleSeconds;
    }

    public override void ChangeBGColor()
    {
        base.ChangeBGColor();
        changeObjectColor();
    }

    private void changeObjectColor()
    {
        panelKontrol.PanelChangeColor(colorRateR, colorRateG, colorRateB); //arkaplan ile birlikte panelin de rengini degistir
        spawnKontrol.SquareChangeColor(colorRateR, colorRateG, colorRateB); //arkaplan ile square'in de rengini degistir
    }
}
