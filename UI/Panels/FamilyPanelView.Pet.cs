using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;

namespace FavoriteCims.UI.Panels
{
    public sealed partial class FamilyPanelView
    {
        public void RenderPet(FamilyPetData pet)
        {
            if (!IsReady) return;
            BubbleFamilyBarDogButton.texture = TextureDB.BubbleDogDisabled;
            BubbleFamilyBarDogButton.tooltip = string.Empty;
            BubbleFamilyBarDogButton.playAudioEvents = false;

            if (pet == null)
            {
                return;
            }

            if (!pet.HasPet)
            {
                BubbleFamilyBarDogButton.tooltip = pet.State == FamilyPetReadState.Unavailable ? FamilyPanelText.PetUnavailable : FamilyPanelText.PetNotFound;
                return;
            }

            BubbleFamilyBarDogButton.texture = TextureDB.BubbleDog;
            BubbleFamilyBarDogButton.playAudioEvents = true;
            string description = string.IsNullOrEmpty(pet.Name) ? pet.Status : string.IsNullOrEmpty(pet.Status) ? pet.Name : pet.Name + " - " + pet.Status;
            BubbleFamilyBarDogButton.tooltip = string.IsNullOrEmpty(pet.OwnerName) ? description : string.IsNullOrEmpty(description) ? pet.OwnerName : description + " - " + pet.OwnerName;
            BubbleFamilyBarDogButton.tooltip = FamilyPanelText.AppendHint(BubbleFamilyBarDogButton.tooltip, FamilyPanelText.InstanceNavigation);
        }
    }
}