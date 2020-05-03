export class SplitterModel {
    constructor(_viewMode: PanelViewMode, _panelType: PanelType) {
        this.viewMode = _viewMode;
        this.panelType = _panelType;
    }
    viewMode: PanelViewMode;
    panelType: PanelType;
}

export enum PanelViewMode {
    Collapse = 1,
    Expand = 2,
    Close = 3,
    None = 4
}

export enum PanelType {
    Left = 1,
    Right = 2,
    Front = 3
}
