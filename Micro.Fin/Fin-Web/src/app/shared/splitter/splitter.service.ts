import { Injectable, ViewContainerRef, ComponentRef, ComponentFactoryResolver } from '@angular/core';
import { Type } from '@angular/compiler/src/core';
import { SplitterModel, PanelViewMode, PanelType } from './splitter.model';
import { Subject, Observable, of } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class SplitterService {

    private panelLeftContainerRef: ViewContainerRef;
    private panelRightContainerRef: ViewContainerRef;

    private panelLeftComponentRef: ComponentRef<Type>;
    private panelRightComponentRef: ComponentRef<Type>;

    togglePanelStatus$: Subject<SplitterModel> = new Subject<SplitterModel>();

    constructor(private componentFactoryResolver: ComponentFactoryResolver) { }

    initializePanel(panelLeftContainerRef: ViewContainerRef, panelRightContainerRef: ViewContainerRef) {
        this.panelLeftContainerRef = panelLeftContainerRef;
        this.panelRightContainerRef = panelRightContainerRef;
    }

    loadLeftPanel(
        componentType: Type, panelViewMode: PanelViewMode = PanelViewMode.Collapse, cfr?: ComponentFactoryResolver,
        togglePanelView: boolean = true)
        : Observable<ComponentRef<any>> {
        const splitterModel = new SplitterModel(panelViewMode, PanelType.Left);
        const result = this.loadPanel(componentType, this.panelLeftContainerRef, splitterModel,
            this.panelLeftComponentRef, cfr, togglePanelView);
        if (result) {
            this.panelLeftComponentRef = result as ComponentRef<Type>;
        }
        return of(this.panelLeftComponentRef);
    }

    loadRightPanel(
        componentType: Type, panelViewMode: PanelViewMode = PanelViewMode.Collapse, cfr?: ComponentFactoryResolver,
        togglePanelView: boolean = true)
        : Observable<ComponentRef<any>> {
        const splitterModel = new SplitterModel(panelViewMode, PanelType.Right);
        const result = this.loadPanel(componentType, this.panelRightContainerRef, splitterModel,
          this.panelRightComponentRef, cfr, togglePanelView);
        if (result) {
          this.panelRightComponentRef = result as ComponentRef<Type>;
        }
        return of(this.panelRightComponentRef);
    }

    closeLeftPanel(unloadPanelData: boolean = false) {
        if (unloadPanelData) {
            this.unloadPanelComponent(this.panelLeftComponentRef);
        }
        this.togglePanelStatus$.next(new SplitterModel(PanelViewMode.Close, PanelType.Left));
    }

    closeRightPanel(unloadPanelData: boolean = false, defaultRightPanelComponent: any = null, factoryResolver: ComponentFactoryResolver = null) {
        if (defaultRightPanelComponent) {
            this.unloadPanelComponent(this.panelRightComponentRef);
            return this.loadRightPanel(defaultRightPanelComponent, PanelViewMode.Collapse, factoryResolver);
        }
        if (unloadPanelData) {
            this.unloadPanelComponent(this.panelRightComponentRef);
        }
        this.togglePanelStatus$.next(new SplitterModel(PanelViewMode.Close, PanelType.Right));
    }

    private loadPanel(componentType: Type, containerRef: ViewContainerRef, splitterModel: SplitterModel,
        componentRef: ComponentRef<Type>, cfr?: ComponentFactoryResolver, togglePanelView: boolean = true): ComponentRef<any> | boolean {
        try {
            this.unloadPanelComponent(componentRef);
            if (undefined !== cfr) {
                const component = cfr.resolveComponentFactory(componentType);
                componentRef = containerRef.createComponent(component);
            } else {
                const component = this.componentFactoryResolver.resolveComponentFactory(componentType);
                componentRef = containerRef.createComponent(component);
            }

            if (togglePanelView) {
                this.togglePanelStatus$.next(splitterModel);
            }

            return componentRef;
        } catch (e) {
            console.log(e);
            return false;
        }
    }

    private unloadPanelComponent(panelRef: ComponentRef<Type>) {
        if (panelRef) {
            panelRef.destroy();
        }
    }
}
