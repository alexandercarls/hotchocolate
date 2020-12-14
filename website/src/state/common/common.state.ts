export interface CommonState {
  expandedPaths: string[];
  searchQuery: string;
  showAside: boolean;
  showCookieConsent: boolean;
  showTOC: boolean;
  showLegacyDocInfo: boolean;
  yScrollPosition: number;
}

export const initialState: CommonState = {
  expandedPaths: [],
  searchQuery: "",
  showAside: false,
  showCookieConsent: false,
  showTOC: false,
  showLegacyDocInfo: false,
  yScrollPosition: 0,
};
