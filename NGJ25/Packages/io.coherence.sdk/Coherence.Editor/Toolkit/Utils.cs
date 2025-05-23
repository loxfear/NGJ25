// Copyright (c) coherence ApS.
// See the license file in the package root for more information.

namespace Coherence.Editor.Toolkit
{
    using UnityEditor;
    using UnityEngine;
    using Coherence.Toolkit;
    using Coherence.Simulator;
    using Coherence.UI;
    using UnityEditor.SceneManagement;

    internal static class Utils
    {
        public static GameObject CreateInstance<T>(MenuCommand menuCommand, string name) where T : Component
        {
            GameObject parent = menuCommand != null ? menuCommand.context as GameObject : null;
            var go = ObjectFactory.CreateGameObject(name, typeof(T));
            GameObjectCreationCommands.Place(go, parent);
            return go;
        }

        // global

        [MenuItem("GameObject/coherence/Coherence Bridge", false, 10)]
        public static void AddBridgeInstanceInScene(MenuCommand menuCommand)
        {
            Analytics.Capture(Analytics.Events.MenuItem, ("menu", "gameobject"), ("item", "bridge"));
            _ = CreateInstance<CoherenceBridge>(menuCommand, "coherence Bridge");
            MessageQueue.AddToQueue((new GUIContent($"{nameof(CoherenceBridge)} was added to {EditorSceneManager.GetActiveScene().name}")));
        }

        // queries

        [MenuItem("GameObject/coherence/Queries/Live Query", false, 20)]
        public static void AddLiveQueryInstanceInScene(MenuCommand menuCommand)
        {
            Analytics.Capture(Analytics.Events.MenuItem, ("menu", "gameobject"), ("item", "live_query"));
            _ = CreateInstance<CoherenceLiveQuery>(menuCommand, "coherence Live Query");
            MessageQueue.AddToQueue((new GUIContent($"{nameof(CoherenceLiveQuery)} was added to {EditorSceneManager.GetActiveScene().name}")));
        }

        [MenuItem("GameObject/coherence/Queries/Tag Query", false, 21)]
        public static void AddTagQueryInstanceInScene(MenuCommand menuCommand)
        {
            Analytics.Capture(Analytics.Events.MenuItem, ("menu", "gameobject"), ("item", "tag_query"));
            _ = CreateInstance<CoherenceTagQuery>(menuCommand, "coherence Tag Query");
        }

        // scene loading

        [MenuItem("GameObject/coherence/Scene Loading/Coherence Scene Loader", false, 30)]
        public static void AddCoherenceSceneLoaderInstanceInScene(MenuCommand menuCommand)
        {
            Analytics.Capture(Analytics.Events.MenuItem, ("menu", "gameobject"), ("item", "scene_loader"));
            _ = CreateInstance<CoherenceSceneLoader>(menuCommand, "coherence Scene Loader");
        }

        [MenuItem("GameObject/coherence/Scene Loading/Coherence Scene", false, 31)]
        public static void AddCoherenceSceneInstanceInScene(MenuCommand menuCommand)
        {
            Analytics.Capture(Analytics.Events.MenuItem, ("menu", "gameobject"), ("item", "scene"));
            _ = CreateInstance<CoherenceScene>(menuCommand, "coherence Scene");
        }

        // events

        [MenuItem("GameObject/coherence/Events/Connection Event Handler", false, 40)]
        public static void AddConnectionEventHandlerInstanceInScene(MenuCommand menuCommand)
        {
            Analytics.Capture(Analytics.Events.MenuItem, ("menu", "gameobject"), ("item", "connection_event_handler"));
            _ = CreateInstance<ConnectionEventHandler>(menuCommand, "coherence Connection Event Handler");
        }

        [MenuItem("GameObject/coherence/Events/Simulator Event Handler", false, 41)]
        public static void AddSimulatorEventHandlerInstanceInScene(MenuCommand menuCommand)
        {
            Analytics.Capture(Analytics.Events.MenuItem, ("menu", "gameobject"), ("item", "simulator_event_handler"));
            _ = CreateInstance<SimulatorEventHandler>(menuCommand, "coherence Simulator Event Handler");
        }

        // mrs

        [MenuItem("GameObject/coherence/Multi-Room Simulators/Multi-Room Simulator", false, 50)]
        public static void AddSimulatorRoomJoinerInstanceInScene(MenuCommand menuCommand)
        {
            Analytics.Capture(Analytics.Events.MenuItem, ("menu", "gameobject"), ("item", "mrs"));
            _ = CreateInstance<MultiRoomSimulator>(menuCommand, "coherence Multi-Room Simulator");
        }

        [MenuItem("GameObject/coherence/Multi-Room Simulators/Local Forwarder", false, 51)]
        public static void AddMultiRoomSimulatorLocalForwarderInstanceInScene(MenuCommand menuCommand)
        {
            Analytics.Capture(Analytics.Events.MenuItem, ("menu", "gameobject"), ("item", "mrs_local_forwarder"));
            _ = CreateInstance<MultiRoomSimulatorLocalForwarder>(menuCommand, "coherence Multi-Room Simulator Local Forwarder");
        }

        // sims

        [MenuItem("GameObject/coherence/Simulators/Auto Simulator Connection", false, 60)]
        public static void AddAutoSimulatorConnection(MenuCommand menuCommand)
        {
            Analytics.Capture(Analytics.Events.MenuItem, ("menu", "gameobject"), ("item", "auto_sim_connection"));
            _ = CreateInstance<AutoSimulatorConnection>(menuCommand, "coherence Auto Simulator Connection");
        }

        [MenuItem("GameObject/coherence/Add Connect Dialog...", false, 100)]
        public static void AddSampleDialogInScene(MenuCommand menuCommand)
        {
            Analytics.Capture(Analytics.Events.MenuItem, ("menu", "gameobject"), ("item", "connect_dialog"));
            SampleDialogPickerWindow.ShowWindow("Connect Dialog", menuCommand.context as GameObject);
        }
    }
}
