using UnityEngine;

public class gaol : MonoBehaviour
{
    [SerializeField] private GameObject effectGoalprefab;

    // �G�t�F�N�g�̔����ʒu�i�S�[���̉��F��ʊO���j
    [SerializeField] private Transform effectSpawnPoint;

    // �G�t�F�N�g�������������i�X�e�[�W���F��ʓ����j
    [SerializeField] private Transform effectTargetPoint;

    private int remainingRedCells = -1;   // �܂��S�[�����Ă��Ȃ��Ԍ���(Player�^�O)�̎c��
    private bool goalCompleted = false;   // �S�[�����o�i�G�t�F�N�g�E�V�[���i�s�j���ς񂾂�

    private void Start()
    {
        // �V�[���J�n���ɑ��݂���uPlayer�v�^�O�i�Ԍ����j�̑����𐔂��Ă���
        // �d�l�F�u�S�Ă̐Ԍ������S�[���ɓ�������v�G�t�F�N�g���o���A�̔����ɂ���
        remainingRedCells = GameObject.FindGameObjectsWithTag("Player").Length;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player") return;
        if (goalCompleted) return; // ���ɑS���S�[���ς݂Ȃ牽�����Ȃ�

        // ���̐Ԍ������l�������_�f���� GameManager �ɉ��Z����i�Ԍ��������邽�і���s���j
        Player playerScript = other.GetComponent<Player>();
        if (playerScript != null)
        {
            GameManager.Instance.AddOxygen(playerScript.Oxygyn_get);
        }

        remainingRedCells--;

        // �܂��S�[�����Ă��Ȃ��Ԍ������c���Ă���΂����ŏI��
        // �G�t�F�N�g�����ƃS�[�������i�V�[���i�s�j�́u�S�������v�̏u�Ԃ����s��
        if (remainingRedCells > 0) return;

        goalCompleted = true;

        // ===== ��������u�S�Ă̐Ԍ������S�[���ɓ������v�u�Ԃ̏��� =====
        if (effectGoalprefab != null)
        {
            // �S�[�����̈ʒu�ɃG�t�F�N�g�𐶐�
            Vector3 spawnPos = effectSpawnPoint != null
                ? effectSpawnPoint.position
                : transform.position;

            // �X�e�[�W���֌����ĉ�]�i�X�}�u���̌��ăG�t�F�N�g�Ɠ����A��ʊO����ʓ��̃C���[�W�j
            Vector3 direction = effectTargetPoint != null
                ? (effectTargetPoint.position - spawnPos).normalized
                : Vector3.up;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Instantiate(effectGoalprefab, spawnPos, rotation);
        }

        Invoke("GoalAfterDelay", 0.5f); // 0.5�b��
    }

    private void GoalAfterDelay()
    {
        GameManager.Instance.OnGoalReached();
    }
}