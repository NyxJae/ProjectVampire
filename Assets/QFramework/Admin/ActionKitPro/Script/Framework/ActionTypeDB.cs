/****************************************************************************
 * Copyright (c) 2020.10 ~ 12 liangxie
 * 
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace QFramework
{
    public class ActionTypeDB
    {
        private static Dictionary<string, Type> mDB = null;

        static Dictionary<string, Type> MakeSureDB()
        {
            if (mDB == null)
            {
                Search();
            }

            return mDB;
        }
        
        public static IEnumerable<Type> GetAll()
        {
            return MakeSureDB().Values;
        }

        [Obsolete("这个以后要迭代掉")]
        public static Type GetTypeByFullName(string fullName)
        {
            return MakeSureDB()[fullName];
        }
        
        static void Search()
        {
            var actionType = typeof(ActionKitVisualAction);
            
            mDB = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
                {
                    try
                    {
                        return a.GetTypes();
                    }
                    catch (Exception _)
                    {
                        return new Type[] { };
                    }
                })
                .Where(t => !t.IsAbstract && actionType.IsAssignableFrom(t))
                .ToDictionary(t => t.FullName);

        }
    }
}