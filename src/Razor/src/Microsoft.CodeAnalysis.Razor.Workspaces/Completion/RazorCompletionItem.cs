﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

#nullable disable

using System;
using System.Linq;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Internal;
using System.Collections.Generic;

namespace Microsoft.CodeAnalysis.Razor.Completion
{
    internal sealed class RazorCompletionItem : IEquatable<RazorCompletionItem>
    {
        private ItemCollection _items;

        /// <summary>
        /// Creates a new Razor completion item
        /// </summary>
        /// <param name="displayText">The text to display in the completion list</param>
        /// <param name="insertText">Content to insert when completion item is committed</param>
        /// <param name="kind">The type of completion item this is. Used for icons and resolving extra information like tooltip text.</param>
        /// <param name="sortText">A string that is used to alphabetically sort the completion item. If omitted defaults to <paramref name="displayText"/>.</param>
        /// <param name="commitCharacters">Characters that can be used to commit the completion item.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="displayText"/> or <paramref name="insertText"/> are <c>null</c>.</exception>
        /// <param name="isSnippet">Indicates whether the completion item's <see cref="InsertText"/> is an LSP snippet or not.</param>
        public RazorCompletionItem(
            string displayText,
            string insertText,
            RazorCompletionItemKind kind,
            string sortText = null,
            IReadOnlyList<RazorCommitCharacter> commitCharacters = null,
            bool isSnippet = false)
        {
            if (displayText is null)
            {
                throw new ArgumentNullException(nameof(displayText));
            }

            if (insertText is null)
            {
                throw new ArgumentNullException(nameof(insertText));
            }

            DisplayText = displayText;
            InsertText = insertText;
            Kind = kind;
            CommitCharacters = commitCharacters;
            SortText = sortText ?? displayText;
            IsSnippet = isSnippet;
        }

        public string DisplayText { get; }

        public string InsertText { get; }

        public bool IsSnippet { get; }

        /// <summary>
        /// A string that is used to alphabetically sort the completion item.
        /// </summary>
        public string SortText { get; }

        public RazorCompletionItemKind Kind { get; }

        public IReadOnlyCollection<RazorCommitCharacter> CommitCharacters { get; }

        public ItemCollection Items
        {
            get
            {
                if (_items is null)
                {
                    lock (this)
                    {
                        _items ??= new ItemCollection();
                    }
                }

                return _items;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RazorCompletionItem);
        }

        public bool Equals(RazorCompletionItem other)
        {
            if (other is null)
            {
                return false;
            }

            if (!string.Equals(DisplayText, other.DisplayText, StringComparison.Ordinal))
            {
                return false;
            }

            if (!string.Equals(InsertText, other.InsertText, StringComparison.Ordinal))
            {
                return false;
            }

            if (Kind != other.Kind)
            {
                return false;
            }

            if (!Enumerable.SequenceEqual(Items, other.Items))
            {
                return false;
            }

            if ((CommitCharacters is null ^ other.CommitCharacters is null) ||
                (CommitCharacters is not null && other.CommitCharacters is not null &&
                    !CommitCharacters.SequenceEqual(other.CommitCharacters)))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashCodeCombiner = HashCodeCombiner.Start();
            hashCodeCombiner.Add(DisplayText);
            hashCodeCombiner.Add(InsertText);
            hashCodeCombiner.Add(Kind);
            hashCodeCombiner.Add(CommitCharacters);

            return hashCodeCombiner.CombinedHash;
        }
    }
}
