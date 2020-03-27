using UnityEngine;
using TMPro;

public class Item_Skillpoint : BaseItem {

    private TextMeshProUGUI valueUI;
    private new ParticleSystem particleSystem;

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.ItemSkillPoint;
        particleSystem = GetComponent<ParticleSystem>();

        valueUI = transform.FindChild<TextMeshProUGUI>("Value");

        if (PersistentData.instance.enableParticleSystems)
            particleSystem.Play();
        else
            particleSystem.Stop();

        int random = Random.Range(0, 100);
        if (random < 3)
            SetValue(Random.Range(5, 11), valueUI);
        else
            SetValue(Random.Range(1, 4), valueUI);

        EventManager.StartListening("ToggleParticleSystems", OnToggleParticleSystems);
    }

    private void OnToggleParticleSystems() {
        if (PersistentData.instance.enableParticleSystems)
            particleSystem.Play();
        else
            particleSystem.Stop();
    }

    protected override void OnItemCollected() {
        Score.instance.IncSkillPoints(value);
    }
}
