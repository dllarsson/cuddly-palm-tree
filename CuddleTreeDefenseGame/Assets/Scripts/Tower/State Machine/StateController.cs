using System;
using System.Collections.Generic;
using System.Linq;

namespace StateMachine
{
    public class StateController
    {
        private IState currentState;

        private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> anyTransitions = new List<Transition>();
        private List<Transition> currentTransitions = new List<Transition>();
        private List<(Transition transition, bool toggle)> subStateTransitions = new List<(Transition, bool)>();

        private Dictionary<IState, (bool running, Func<bool> condition)> dictSubStates = new Dictionary<IState, (bool, Func<bool>)>();

        public void Update()
        {
            var transition = GetTransition();
            if(transition != null)
            {
                SetState(transition.To);
            }
            currentState?.Update();

            ToggleSubStates();
            foreach(var subState in dictSubStates.Where(subState => subState.Value.running))
                subState.Key.Update();
        }
        private Transition GetTransition()
        {
            var anyTransition = anyTransitions.Where(transition => transition.Condition()).FirstOrDefault();
            return anyTransition ?? currentTransitions.Where(transition => transition.Condition()).FirstOrDefault();
        }
        public void SetState(IState state)
        {
            if(state == currentState || (dictSubStates.TryGetValue(state, out var value) && value.running))
                return;
            currentState?.End();
            currentState = state;

            transitions.TryGetValue(currentState.GetType(), out currentTransitions);
            if(currentTransitions == null)
                currentTransitions.Clear();

            currentState.Start();
        }
        private void ToggleSubStates()
        {
            foreach(var subState in dictSubStates
                .Where(subState => subState.Value.running != subState.Value.condition() && subState.Key != currentState)
                .ToArray())
            {
                dictSubStates[subState.Key] = (!subState.Value.running, subState.Value.condition);
                if(!subState.Value.running)
                    subState.Key.Start();
                else
                    subState.Key.End();
            }
        }
        public void AddTransition(IState state, Func<bool> condition)
        {
            anyTransitions.Add(new Transition(state, condition));
        }

        public void AddTransition(IState fromState, IState toState, Func<bool> condition)
        {
            if(!transitions.TryGetValue(fromState.GetType(), out var transition))
            {
                transition = new List<Transition>();
                transitions[fromState.GetType()] = transition;
            }
            transition.Add(new Transition(toState, condition));
        }
        public void Substate(IState state, Func<bool> condition)
        {
            if(!dictSubStates.ContainsKey(state))
                dictSubStates.Add(state, (false, condition));
        }

        public class Transition
        {
            public Func<bool> Condition { get; }
            public IState To { get; }
            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }
    }
}