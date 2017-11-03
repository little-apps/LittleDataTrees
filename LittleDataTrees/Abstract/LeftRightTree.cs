using System;
using System.IO;
using LittleDataTrees.Enumerators;
using LittleDataTrees.Misc;

namespace LittleDataTrees.Abstract
{
    public abstract class LeftRightTree<TNode, TValue> : BaseTree<TNode, TValue>
        where TNode : LeftRightNode<TNode, TValue>
        where TValue : IComparable<TValue> 
    {
        public InOrderEnumerator<TNode, TValue> InOrder => new InOrderEnumerator<TNode, TValue>(this);
        public PreOrderEnumerator<TNode, TValue> PreOrder => new PreOrderEnumerator<TNode, TValue>(this);
        public PostOrderEnumerator<TNode, TValue> PostOrder => new PostOrderEnumerator<TNode, TValue>(this);

