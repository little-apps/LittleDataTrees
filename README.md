# Little Data Trees
[![Build Status](https://travis-ci.org/little-apps/LittleDataTrees.svg?branch=master)](https://travis-ci.org/little-apps/LittleDataTrees)

This project demonstrates various tree structures using the C# programming language. It is a public domain project.

## License

Little Data Trees is free and open source, and is licensed under the MIT License.

 > Copyright (c) 2017 Little Apps
 > 
 > Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
 > 
 > The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
 > 
 > THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

## Trees

All trees implement the ``BaseTree<TNode, TValue>`` abstract class with methods to add, delete, and find nodes in the tree. There is the ability to traverse trees using pre-order, in-order, and post-order and output them horizontally or vertically as text.

### AVL

The AVL tree is one of the most popular self-balancing binary search trees. It is named after it's two inventors, Georgy Adelson-Velsky and Evgenii Landis. When the tree becomes unbalanced, a single or double rotation (depending on the node balances) is performed to balance it.

### BST

A binary search tree (BST) is unbalanced and the most basic of the data trees. Since they are the most basic, they are easiest to code. 

## Show Your Support

Little Apps relies on people like you to keep our software running. If you would like to show your support for Little Data Trees, then you can [make a donation](https://www.little-apps.com/?donate) using PayPal, Payza or credit card (via Stripe). Please note that any amount helps (even just $1).
