namespace HunieMod;

/// <summary>
/// A collection of helpers and method wrappers for the <see cref="Girl"/> class.
/// </summary>
public static class GirlExtensions
{
    /// <summary>
    /// Adds all the <see cref="GirlPieceArt"/> contained in the specified <see cref="GirlPiece"/> to the correct container/layer of this girl.
    /// </summary>
    /// <param name="girl">The <see cref="Girl"/> instance the piece will be added to.</param>
    /// <param name="girlPiece">The piece to be added to the <see cref="Girl"/> instance. Most can be obtained using <see cref="GirlDefinition.GetPiecesByType"/>.</param>
    /// <remarks>
    /// Note that this expects that the <see cref="GirlPiece.art"/> (specifically <see cref="GirlPieceArt.spriteName"/>) values exist in <see cref="Girl.spriteCollection"/>.
    /// All the existing children of the container where the piece will be added to will be destroyed.
    /// </remarks>
    public static void AddGirlPiece(this Girl girl, GirlPiece girlPiece)
    {
        AccessTools.Method(typeof(Girl), nameof(AddGirlPiece)).Invoke(girl, [girlPiece]);
    }

    /// <summary>
    /// Creates a <see cref="SpriteObject"/> from the specified <see cref="GirlPieceArt"/> and adds it to the specified container.
    /// </summary>
    /// <param name="girl">The <see cref="Girl"/> instance the piece art will be added to.</param>
    /// <param name="girlPieceArt">The piece art to be added to the <see cref="Girl"/> instance.</param>
    /// <param name="container">The container to which to add the piece art to. All the existing children of the container will be destroyed.</param>
    /// <remarks>Note that this expects that the <see cref="GirlPieceArt.spriteName"/> value exist in <see cref="Girl.spriteCollection"/>.</remarks>
    public static void AddGirlPieceArtToContainer(this Girl girl, GirlPieceArt girlPieceArt, DisplayObject container)
    {
        AccessTools.Method(typeof(Girl), nameof(AddGirlPieceArtToContainer)).Invoke(girl, [girlPieceArt, container]);
    }

    /// <summary>
    /// Adds the specified sprite object to the specified container, using the sprite's metadata from the specified piece art.
    /// </summary>
    /// <param name="girl">The <see cref="Girl"/> instance the sprite object will be added to (only used for alt. girl checking).</param>
    /// <param name="sprite">The sprite object to add.</param>
    /// <param name="pieceArt">The piece art with metadata for the sprite object.</param>
    /// <param name="container">The container to which to add the sprite object to.</param>
    /// <param name="removeChildren">Whether to remove and destroy all existing children in the container.</param>
    /// <remarks>
    /// An alternative way to add sprites to any container, making it possible to manually manage the <see cref="tk2dBaseSprite.Collection"/>
    /// and the option to keep any existing children in the container.
    /// </remarks>
    public static void AddSpriteObjectToContainer(this Girl girl, SpriteObject sprite, GirlPieceArt pieceArt, DisplayObject container, bool removeChildren = true)
    {
        if (sprite != null && pieceArt != null && container != null && !pieceArt.spriteName.IsNullOrWhiteSpace())
        {
            if (removeChildren && container.GetChildren().Length > 0)
            {
                container.RemoveAllChildren(true);
            }

            container.AddChild(sprite);
            sprite.sprite.FlipX = girl.flip;
            int offsetX = girl.flip ? GameCamera.SCREEN_DEFAULT_WIDTH : 0;
            sprite.SetLocalPosition(Mathf.Abs(offsetX - pieceArt.x), -pieceArt.y);
        }
    }

    /// <summary>
    /// Forces a facial expression on the girl.
    /// </summary>
    /// <param name="girl">The <see cref="Girl"/> instance on which to set the expression on.</param>
    /// <param name="expressionType">The type of expression to set.</param>
    /// <param name="changeEyes">When <c>true</c>, also set the girl's eyes to the one that belongs to the specified expression.</param>
    /// <param name="changeMouth">When <c>true</c>, also set the girl's mouth to the one that belongs to the specified expression.</param>
    /// <remarks>
    /// The girl's eyebrows and face/blush are always set.
    /// When the specified expression is not found, the girl's <see cref="GirlDefinition.defaultExpression"/> will be used.
    /// </remarks>
    public static void ForceExpression(this Girl girl, GirlExpressionType expressionType, bool changeEyes = true, bool changeMouth = true)
    {
        if (girl.definition == null)
        {
            throw new InvalidOperationException($"{nameof(girl)}.{nameof(girl.definition)} is null");
        }

        int index = girl.definition.pieces.FindIndex(p => p.type == GirlPieceType.EXPRESSION && p.expressionType == expressionType);
        if (index == -1)
        {
            index = girl.definition.defaultExpression;
        }

        girl.ForceExpression(index, changeEyes, changeMouth);
    }

    /// <summary>
    /// Forces a facial expression on the specified girl.
    /// </summary>
    /// <param name="girl">The <see cref="Girl"/> instance on which to set the expression on.</param>
    /// <param name="pieceIndex">Which index of <see cref="GirlDefinition.pieces"/> to set.</param>
    /// <param name="changeEyes">When <c>true</c>, also set the girl's eyes to the one that belongs to the specified expression.</param>
    /// <param name="changeMouth">When <c>true</c>, also set the girl's mouth to the one that belongs to the specified expression.</param>
    /// <remarks>
    /// The girl's eyebrows and face/blush are always set.
    /// When the specified piece is not found or it's <see cref="GirlPiece.type"/> is not <see cref="GirlPieceType.EXPRESSION"/>,
    /// the girl's <see cref="GirlDefinition.defaultExpression"/> will be used.
    /// </remarks>
    public static void ForceExpression(this Girl girl, int pieceIndex, bool changeEyes = true, bool changeMouth = true)
    {
        if (pieceIndex < 0 || pieceIndex >= girl.definition.pieces.Count)
        {
            throw new IndexOutOfRangeException(nameof(pieceIndex));
        }

        GirlPiece piece = girl.definition.pieces[pieceIndex];

        if (piece == null || piece.type != GirlPieceType.EXPRESSION)
        {
            piece = girl.definition.pieces[girl.definition.defaultExpression];
        }

        if (piece != null)
        {
            if (changeEyes)
            {
                girl.eyes.RemoveAllChildren();
            }

            girl.eyebrows.RemoveAllChildren();
            if (changeMouth)
            {
                girl.mouth.RemoveAllChildren();
            }

            girl.face.RemoveAllChildren();

            if (changeEyes)
            {
                girl.AddGirlPieceArtToContainer(piece.primaryArt, girl.eyes);
            }

            girl.AddGirlPieceArtToContainer(piece.secondaryArt, girl.eyebrows);
            if (changeMouth)
            {
                girl.AddGirlPieceArtToContainer(piece.tertiaryArt, girl.mouth);
            }

            girl.AddGirlPieceArtToContainer(piece.quaternaryArt, girl.face);

            girl.ChangeExpression(piece.expressionType, true, changeEyes, changeMouth, 0.5f);
        }
    }

    /// <summary>
    /// Gets the container for the specified <see cref="GirlLayer"/> for this girl.
    /// </summary>
    /// <param name="girl">The <see cref="Girl"/> instance the container belongs to.</param>
    /// <param name="layer">The layer to get the container for.</param>
    /// <returns>A <see cref="DisplayObject"/> to be used for the specified <see cref="GirlLayer"/>.</returns>
    public static DisplayObject GetContainerByLayer(this Girl girl, GirlLayer layer)
    {
        return (DisplayObject)AccessTools.Method(typeof(Girl), nameof(GetContainerByLayer)).Invoke(girl, [layer]);
    }

    /// <summary>
    /// Gets the girl piece for the specified <see cref="GirlExpressionType"/> for this girl.
    /// </summary>
    /// <param name="girl">The <see cref="Girl"/> instance for which to get the piece.</param>
    /// <param name="expression">The expression type for which to get the <see cref="GirlPiece"/> for.</param>
    /// <returns>The girl piece meant for the specified expression.</returns>
    public static GirlPiece GetGirlPieceByExpressionType(this Girl girl, GirlExpressionType expression)
    {
        return (GirlPiece)AccessTools.Method(typeof(Girl), nameof(GetGirlPieceByExpressionType)).Invoke(girl, [expression]);
    }

    /// <summary>
    /// Set the mouth shape of this girl to the shape that goes with the phoneme of the specified letter (A-Z). Case-insensitive.
    /// </summary>
    /// <param name="girl">The <see cref="Girl"/> instance on which to apply the mouth shape to.</param>
    /// <param name="letter">The letter to set the mouth shape to.</param>
    public static void MouthLetter(this Girl girl, string letter)
    {
        AccessTools.Method(typeof(Girl), nameof(MouthLetter)).Invoke(girl, [letter]);
    }

    /// <summary>
    /// Resets the expression for this girl to the defaults of the expression type that was last set using <see cref="Girl.ChangeExpression"/>.
    /// </summary>
    /// <param name="girl">The <see cref="Girl"/> instance for which to reset the expression.</param>
    public static void ResetExpression(this Girl girl)
    {
        AccessTools.Method(typeof(Girl), nameof(ResetExpression)).Invoke(girl, null);
    }
}
