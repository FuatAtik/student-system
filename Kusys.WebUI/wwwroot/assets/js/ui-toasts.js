'use strict';

(function () {
  const toastPlacementExample = document.querySelector('.toast-placement-ex')
  let selectedType, selectedPlacement, toastPlacement;

  function toastDispose(toast) {
    if (toast && toast._element !== null) {
      if (toastPlacementExample) {
        toastPlacementExample.classList.remove(selectedType);
        DOMTokenList.prototype.remove.apply(toastPlacementExample.classList, selectedPlacement);
      }
      toast.dispose();
    }
  }
  if (toastPlacement) {
    toastDispose(toastPlacement);
  }
  selectedType = "bg-success";
  selectedPlacement = ["top-0","end-0"];

  toastPlacementExample.classList.add(selectedType);
  DOMTokenList.prototype.add.apply(toastPlacementExample.classList, selectedPlacement);
  toastPlacement = new bootstrap.Toast(toastPlacementExample);
  toastPlacement.show();
})();