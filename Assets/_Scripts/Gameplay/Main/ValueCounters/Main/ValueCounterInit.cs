using PaleLuna.Architecture.Initializer;
using System.Threading;
using Cysharp.Threading.Tasks;
using Services;

public class ValueCounterInit : IInitializer
{
    private CancellationTokenSource _tokenSource = new CancellationTokenSource();

    private InitStatus _initStatus = InitStatus.Shutdown;
    public InitStatus status => _initStatus;

    public void StartInit()
    {
        _initStatus = InitStatus.Initialization;

        _ = Init(_tokenSource.Token);
    }

    private async UniTaskVoid Init(CancellationToken token)
    {
        ValueCounterHolder valueCounterHolder = new();

        await UniTask.Yield(token);

        valueCounterHolder.Registarion(new Wallet());
        valueCounterHolder.Registarion(new HealthCounter());
        valueCounterHolder.Registarion(new EnemyKillCounter());
        valueCounterHolder.Registarion(new ScoreCounter());

        ServiceManager.Instance.SceneLocator.Registarion(valueCounterHolder);

        await UniTask.Yield(token);

        _initStatus = InitStatus.Done;
    }


}
