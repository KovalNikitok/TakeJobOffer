// import {
//   GetStaticProps,
//   GetStaticPaths,
// } from 'next'

// import Title from "antd/es/typography/Title";
// import { useEffect, useState } from "react";
// import { getProfessionSkillsWithNames, getProfessionSkills } from "../../services/professionSkills";
// import { ProfessionSkills } from "../../components/ProfessionSkills";
import { getAllProfessionsISR60 } from "../../services/professions";
import ProfessionSkillsClient  from "./ProfessionsSkillsPage";

export const revalidate = 60;
export const dynamicParams = true;

export async function generateStaticParams () {
  let professions: Profession[];
  try {
    professions = await getAllProfessionsISR60(); 
    return professions.map(profession => ({
      params: { professionId: profession.id },
    }));
  }
  catch (error){
    console.log("Profession not found!" + `\n ${error}`);
    return [{ params: { professionId: "" }}];
  }
}

export default async function ProfessionSkillsPage({ params }: { params: { professionId: string } }) {
  const { professionId } = params;

  return (
    <ProfessionSkillsClient professionId={professionId} />
  );
}

// export async function getStaticProps({ profession }: { profession: Profession })  {
//   return ({
//     props: {
//       profession,
//     },
//     revalidate: revalidate
//   });
// }

// interface ProfessionSkillsProps {
//   params: { profession: Profession };
// }

// export default function ProfessionSkillsPage({ params }: ProfessionSkillsProps) {
//   const [professionSkills, setProfessionSkills] = useState<ProfessionSkillWithName[]>([]);
//   const [loading, setLoading] = useState(true);

//   useEffect(() => {
//     const getSkillsByProfessionId = async (professionId: string) => {
//         const professionSkills = await getProfessionSkillsWithNames(professionId);

//         setLoading(false);
//         setProfessionSkills(professionSkills);
//     }
    
//     getSkillsByProfessionId(params.profession.id)
//   });

//   return (
//     <div>
//       {loading ? (
//         <Title>Loading...</Title>
//       ) : (
//         <ProfessionSkills 
//             professionSkills={professionSkills}
//         />
//       )}
//     </div>
//   );
// };