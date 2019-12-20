using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MathJaxBlazor
{
    public class TexInputSettings
    {
        [JsonPropertyName("inlineMath")]
        /// <summary>
        /// start/end delimiter pairs for in-line math
        /// </summary>
        public List<string[]> InlineMath { get; set; } = new List<string[]> { new string[] { "\\(", "\\)" } };

        [JsonPropertyName("displayMath")]
        /// <summary>
        /// start/end delimiter pairs for display math
        /// </summary>
        public List<string[]> DisplayMath { get; set; } = new List<string[]> { new string[] { "$$", "$$" }, new string[] { "\\[", "\\]" } };

        [JsonPropertyName("processEscapes")]
        /// <summary>
        /// use \$ to produce a literal dollar sign
        /// </summary>
        public bool ProcessEscapes { get; set; } = true;

        [JsonPropertyName("processEnvironments")]
        /// <summary>
        /// process \begin{xxx}...\end{xxx} outside math mode
        /// </summary>
        public bool ProcessEnvironments { get; set; } = true;

        [JsonPropertyName("processRefs")]
        /// <summary>
        /// process \ref{...} outside of math mode
        /// </summary>
        public bool ProcessRefs { get; set; } = true;

        [JsonPropertyName("digits")]
        /// <summary>
        /// regex pattern for recognizing numbers
        /// </summary>
        public string Digits { get; set; } = @"/^(?:[0-9]+(?:\{,\}[0-9]{3})*(?:\.[0-9]*)?|\.[0-9]+)/";

        [JsonPropertyName("tags")]
        /// <summary>
        /// This controls whether equations are numbered and how.
        /// </summary>
        public EquationNumberingTag Tags { get; set; } = EquationNumberingTag.None;

        [JsonPropertyName("tagSide")]
        /// <summary>
        /// This specifies the side on which \tag{} macros will place the tags, and on which automatic equation numbers will appear.
        /// </summary>
        public string TagSide { get; set; } = "right";
    }
}
