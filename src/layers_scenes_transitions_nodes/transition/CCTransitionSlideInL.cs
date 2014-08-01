/****************************************************************************
Copyright (c) 2010-2012 cocos2d-x.org
Copyright (c) 2008-2010 Ricardo Quesada
Copyright (c) 2011 Zynga Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/

namespace CocosSharp
{
    public class CCTransitionSlideInL : CCTransitionScene, ICCTransitionEaseScene
    {
        public CCTransitionSlideInL (float t, CCScene scene) : base (t, scene)
        { }
        
        #region ICCTransitionEaseScene Members

        public virtual CCFiniteTimeAction EaseAction(CCFiniteTimeAction action)
        {
            return new CCEaseOut(action, 2.0f);
        }

        #endregion

        /// <summary>
        /// initializes the scenes
        /// </summary>
        protected virtual void InitScenes()
        {
            var bounds = Layer.VisibleBoundsWorldspace;
            InScene.Position = new CCPoint(bounds.Origin.X -(bounds.Size.Width), bounds.Origin.Y);
        }

        /// <summary>
        /// returns the action that will be performed by the incomming and outgoing scene
        /// </summary>
        /// <returns></returns>
        public virtual CCFiniteTimeAction Action()
        {
            var bounds = Layer.VisibleBoundsWorldspace;
            return new CCMoveBy(Duration, new CCPoint(bounds.Size.Width, 0));
        }

        public override void OnEnter()
        {
            base.OnEnter();
            InitScenes();

            CCFiniteTimeAction incAction = Action();
            CCFiniteTimeAction outcAction = Action();

            CCFiniteTimeAction inAction = EaseAction(incAction);
            CCFiniteTimeAction outAction = new CCSequence
                (
                    EaseAction(outcAction),
                    new CCCallFunc((Finish))
                );
            InScene.RunAction(inAction);
            OutScene.RunAction(outAction);
        }

        protected override void SceneOrder()
        {
            IsInSceneOnTop = false;
        }
    }
}