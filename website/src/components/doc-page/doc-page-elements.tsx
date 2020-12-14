import styled, { createGlobalStyle } from "styled-components";

export const MostProminentSection = styled.div`

`;

export const DocPageStickySideBarStyle = `
  display: block;
  align-self: start;
  position: sticky;
  top: 0;
  height: 90vh;
  overflow-y: auto;
  z-index: 25;
`;


export const BodyStyle = createGlobalStyle<{ disableScrolling: boolean }>`
  body {
    overflow-y: ${({ disableScrolling }) =>
      disableScrolling ? "hidden" : "initial"};

    @media only screen and (min-width: 600px) {
      overflow-y: initial;
    }
  }
`;
